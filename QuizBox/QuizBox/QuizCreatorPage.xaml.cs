using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Text.Json;

namespace QuizBox;

public partial class QuizCreatorPage : ContentPage
{
	private string? quizTitle;
    private string? quizDescription;
    private string? path;
    private string? folderPath;

    public QuizCreatorPage()
	{
		InitializeComponent();
	}

    private void onStackLayout_Loaded(object sender, EventArgs e)
    {
        TitleEntry.Text = "";
        DescriptionEntry.Text = "";
    }

    private async void onAddQuestionsClicked(object sender, EventArgs e)
    {
        quizTitle = TitleEntry.Text;
        quizDescription = DescriptionEntry.Text;


        if (!string.IsNullOrEmpty(quizTitle) && !string.IsNullOrEmpty(quizDescription))
        {
            folderPath = FileSystem.AppDataDirectory;
            string temp = quizTitle + ".json";
            path = Path.Combine(folderPath, temp);

            if (!File.Exists(path))
            {
                var quiz = new
                {
                    quiz = new[]
                {
                    new
                    {
                        name = quizTitle,
                        description = quizDescription,
                        questions = new List<object>()
                    }
                }
                };
                var json = JsonSerializer.Serialize(quiz, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);

                await Shell.Current.GoToAsync($"///QuestionCreatorPage?param={path}");
            }
            else
            {
                await Toast.Make("Quiz already exists", ToastDuration.Short).Show();
            }
        }
        else
        {
            await DisplayAlert("Error", "Please enter a title and description for the quiz.", "OK");
        }
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Cancel", "Would you like to cancel the creation of quiz?", "Yes", "No");
        if (answer)
        {
            await Shell.Current.GoToAsync("///MainPage");
            await Toast.Make("Canceling the creation of quiz!", ToastDuration.Short).Show();
        }
    }
}

