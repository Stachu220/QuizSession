using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using QuizBox.Model;
using System.Text.Json;

namespace QuizBox;

[QueryProperty(nameof(Path), "param")]
public partial class QuizStartPage : ContentPage
{
	public string? Path { get; set; }
    private int QNo = 0;
    private int SetOfQuestions = 0;
    public static Root RootToPass;
    public static List<Question> QuestionsRandomized = new List<Question>();
    private List<Question> questionList = new List<Question>();
    public static List<AnsweredQuestion> AnsweredQuestions = new();

    public QuizStartPage()
	{
		InitializeComponent();
        numberOfQuestionsEntry.Text = null;
	}

    private async void onGoBack(object sender, EventArgs e)
    {
        backBtn.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        backBtn.Background = (Color)Application.Current.Resources["Cerulean"];

        await backBtn.ScaleTo(0.85, 180, Easing.CubicIn);
        await backBtn.ScaleTo(1.0, 180, Easing.CubicOut);

        backBtn.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        backBtn.Background = (Color)Application.Current.Resources["PictonBlue"];

        await Shell.Current.GoToAsync($"///QuizListPage");
    }

    private void VerticalStackLayout_Loaded(object sender, EventArgs e)
    {
        string jsonString = File.ReadAllText(Path);
        Root root = JsonSerializer.Deserialize<Root>(jsonString);
        RootToPass = root;

        Quiz quiz = root.Quiz.FirstOrDefault();
        QNo = 0;
        if (quiz != null)
        {
            QuizTitle.Text = quiz.Name;
            QuizDesc.Text = quiz.Description;

            foreach (var question in quiz.Questions)
            {
                QNo++;
                questionList.Add(question);
            }

            numberOfQuestions.Text = QNo.ToString();
            rollQuestionsOrder();
        }

    }

    private void rollQuestionsOrder()
    {
        //roll questions in random order from questionList to QuestionsRandomized
        Random random = new Random();
        if (QuestionsRandomized.Count != 0)
            QuestionsRandomized.Clear();
        int questionListCount = questionList.Count;
        for (int i = 0; i < questionListCount; i++)
        {
            int randomIndex = random.Next(0, questionList.Count);
            QuestionsRandomized.Add(questionList[randomIndex]);
            questionList.RemoveAt(randomIndex);
        }
    }

    private async void onDeleteQuiz(object sender, EventArgs e)
    {
        DeleteFrame.BackgroundColor = Color.FromHex("#B31900");
        DeleteFrame.Background = Color.FromHex("#B31900");

        await DeleteFrame.ScaleTo(0.9, 180, Easing.CubicIn);
        await DeleteFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        DeleteFrame.BackgroundColor = Color.FromHex("#FF2400");
        DeleteFrame.Background = Color.FromHex("#FF2400");

        bool answer = await DisplayAlert("INFO", "Do you want to delete this quiz?", "YES", "NO");
        if (answer)
        {
            File.Delete(Path);
            await Toast.Make("Selected Quiz was deleted!", ToastDuration.Short).Show();
            await Shell.Current.GoToAsync($"///QuizListPage");
        }
    }

    private async void onExport(object sender, EventArgs e)
    {
        exportFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        exportFrame.Background = (Color)Application.Current.Resources["Cerulean"];

        await exportFrame.ScaleTo(0.9, 180, Easing.CubicIn);
        await exportFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        exportFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        exportFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

        if (string.IsNullOrEmpty(Path) || !File.Exists(Path))
        {
            await DisplayAlert("ERROR", "Quiz file not found!", "OK");
            return;
        }

        await Share.RequestAsync(new ShareFileRequest
        {
            Title = "Share Quiz JSON",
            File = new ShareFile(Path)
        });
    }

    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        numberOfQuestionsEntry.Text = "";
        base.OnNavigatingFrom(args);
    }

    private async void onStart(object sender, EventArgs e)
    {
        startFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        startFrame.Background = (Color)Application.Current.Resources["Cerulean"];

        await startFrame.ScaleTo(0.9, 180, Easing.CubicIn);
        await startFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        startFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        startFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

        if(numberOfQuestionsEntry.Text != null || numberOfQuestionsEntry.Text != "")
        {
            try
            {
                SetOfQuestions = Int32.Parse(numberOfQuestionsEntry.Text);

                if(SetOfQuestions > QNo || SetOfQuestions < 1)
                {
                    await DisplayAlert("ERROR", "Selected number of questions can't be equal to 0 or be out of range!", "OK");
                }
                else
                {
                    await Toast.Make("Starting the Quiz. Good luck!", ToastDuration.Short).Show();
                    await Shell.Current.GoToAsync($"///QuestionPage?param={SetOfQuestions}&paramPath={Path}");
                }
            }
            catch
            {
                await DisplayAlert("ERROR", "There was a problem with number of questions!", "OK");
            }
        }
    }

    private async void OnEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender == numberOfQuestionsEntry)
        {
            numberOfQuestionsBorder.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            numberOfQuestionsBorder.StrokeThickness = 4;
            await numberOfQuestionsBorder.ScaleTo(1.05, 120, Easing.CubicOut);
        }
    }

    private async void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (sender == numberOfQuestionsEntry)
        {
            numberOfQuestionsBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            numberOfQuestionsBorder.StrokeThickness = 2;
            await numberOfQuestionsBorder.ScaleTo(1.0, 120, Easing.CubicOut);
        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        if (sender is Entry entry)
            entry.Unfocus();
    }

    protected override bool OnBackButtonPressed()
    {
        numberOfQuestionsEntry.Unfocus();
        return true;
    }
}