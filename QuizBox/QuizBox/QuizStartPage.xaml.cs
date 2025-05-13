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

    public QuizStartPage()
	{
		InitializeComponent();
	}

    private async void onQNo6(object sender, EventArgs e)
	{
        SetOfQuestions = 6;
        await Toast.Make("Starting the Quiz. Good luck!", ToastDuration.Short).Show();
        await Shell.Current.GoToAsync($"///QuestionPage?param={SetOfQuestions}&paramPath={Path}");
    }
    private async void onQNo10(object sender, EventArgs e)
    {
        SetOfQuestions = 10;
        await Toast.Make("Starting the Quiz. Good luck!", ToastDuration.Short).Show();
        await Shell.Current.GoToAsync($"///QuestionPage?param={SetOfQuestions}&paramPath={Path}");
    }
    private async void onQNo15(object sender, EventArgs e)
    {
        SetOfQuestions = 15;
        await Toast.Make("Starting the Quiz. Good luck!", ToastDuration.Short).Show();
        await Shell.Current.GoToAsync($"///QuestionPage?param={SetOfQuestions}&paramPath={Path}");
    }
    private async void onQNo20(object sender, EventArgs e)
    {
        SetOfQuestions = 20;
        await Toast.Make("Starting the Quiz. Good luck!", ToastDuration.Short).Show();
        await Shell.Current.GoToAsync($"///QuestionPage?param={SetOfQuestions}&paramPath={Path}");
    }

    private void onGoBack(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"///QuizListPage");
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
            QNo6.IsEnabled = true;
            QNo10.IsEnabled = true;
            QNo15.IsEnabled = true;
            QNo20.IsEnabled = true;
            QuizTitle.Text = quiz.Name;
            QuizDesc.Text = quiz.Description;
            foreach (var question in quiz.Questions)
            {
                QNo++;
                questionList.Add(question);
            }
            if (QNo < 6)
            {
                QNo6.IsEnabled = false;
            }
            if (QNo < 10)
            {
                QNo10.IsEnabled = false;
            }
            if (QNo < 15)
            {
                QNo15.IsEnabled = false;
            }
            if (QNo < 20)
            {
                QNo20.IsEnabled = false;
            }

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

    private void onDeleteQuiz(object sender, EventArgs e)
    {
        //delete quiz from json file
        File.Delete(Path);
        Toast.Make("Selected Quiz was deleted!", ToastDuration.Short).Show();
        Shell.Current.GoToAsync($"///QuizListPage");
    }

    private async void onExport(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Path) || !File.Exists(Path))
        {
            await DisplayAlert("Error", "Quiz file not found.", "OK");
            return;
        }

        await Share.RequestAsync(new ShareFileRequest
        {
            Title = "Share Quiz JSON",
            File = new ShareFile(Path)
        });
    }
}