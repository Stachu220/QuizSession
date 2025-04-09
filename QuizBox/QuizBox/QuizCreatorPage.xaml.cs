using System.Text.Json;

namespace QuizBox;

public partial class QuizCreatorPage : ContentPage
{
	private string? quizTitle;
    private string? quizDescription;
    private string? path;

    public QuizCreatorPage()
	{
		InitializeComponent();
	}

    private async void onAddQuestionsClicked(object sender, EventArgs e)
    {
        quizTitle = TitleEntry.Text;
        quizDescription = DescriptionEntry.Text;

        if (!string.IsNullOrEmpty(quizTitle) && !string.IsNullOrEmpty(quizDescription))
        {
            path = quizTitle + ".json";

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
            //TODO: Fix file path
            File.WriteAllText(path, json);

            await Shell.Current.GoToAsync("///QuestionCreatorPage");
        }
        else
        {
            await DisplayAlert("Error", "Please enter a title and description for the quiz.", "OK");
        }
    }
}
