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
        addQuestionsFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        addQuestionsFrame.Background = (Color)Application.Current.Resources["Cerulean"];

        await addQuestionsFrame.ScaleTo(0.9, 180, Easing.CubicIn);
        await addQuestionsFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        addQuestionsFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        addQuestionsFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

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
                await Toast.Make("Quiz already exists!", ToastDuration.Short).Show();
            }
        }
        else
        {
            await DisplayAlert("ERROR", "Please enter a title and description for the quiz!", "OK");
        }
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        CancelButton.BackgroundColor = Color.FromHex("#B31900");
        CancelButton.Background = Color.FromHex("#B31900");

        await CancelButton.ScaleTo(0.85, 180, Easing.CubicIn);
        await CancelButton.ScaleTo(1.0, 180, Easing.CubicOut);

        CancelButton.BackgroundColor = Color.FromHex("#FF2400");
        CancelButton.Background = Color.FromHex("#FF2400");

        bool answer = await DisplayAlert("CANCEL", "Would you like to cancel the creation of quiz?", "YES", "NO");
        if (answer)
        {
            await Shell.Current.GoToAsync("///MainPage");
            await Toast.Make("Canceling the creation of quiz!", ToastDuration.Short).Show();
        }
    }

    private void OnClearDescirption_Clicked(object sender, EventArgs e)
    {
        DescriptionEntry.Text = string.Empty;
    }

    private async void OnEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender == TitleEntry)
        {
            TitleEntryBorder.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            TitleEntryBorder.StrokeThickness = 4;
            await TitleEntryBorder.ScaleTo(1.05, 120, Easing.CubicOut);
        }
        else if (sender == DescriptionEntry)
        {
            DescriptionEntryBorder.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            DescriptionEntryBorder.StrokeThickness = 4;
            await DescriptionEntryBorder.ScaleTo(1.05, 120, Easing.CubicOut);
        }
    }

    private async void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (sender == TitleEntry)
        {
            TitleEntryBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            TitleEntryBorder.StrokeThickness = 2;
            await TitleEntryBorder.ScaleTo(1.0, 120, Easing.CubicOut);
        }
        else if (sender == DescriptionEntry)
        {
            DescriptionEntryBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            DescriptionEntryBorder.StrokeThickness = 2;
            await DescriptionEntryBorder.ScaleTo(1.0, 120, Easing.CubicOut);
        }
    }
}

