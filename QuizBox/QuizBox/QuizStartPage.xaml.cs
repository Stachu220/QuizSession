using QuizBox.Model;
using System.Text.Json;

namespace QuizBox;

[QueryProperty(nameof(Path), "param")]
public partial class QuizStartPage : ContentPage
{
	public string? Path { get; set; }
    private int QNo = 0;
    public static Root RootToPass;
    public static List<Question> QuestionsRandomized = new List<Question>();
    private List<Question> questionList = new List<Question>();

    public QuizStartPage()
	{
		InitializeComponent();
	}

    private async void onQNo6(object sender, EventArgs e)
	{

    }
    private async void onQNo10(object sender, EventArgs e)
    {

    }
    private async void onQNo15(object sender, EventArgs e)
    {

    }
    private async void onQNo20(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"///QuestionPage?param={QNo}&paramPath={Path}");
    }

    private void VerticalStackLayout_Loaded(object sender, EventArgs e)
    {
        string jsonString = File.ReadAllText(Path);
        Root root = JsonSerializer.Deserialize<Root>(jsonString);
        RootToPass = root;

        Quiz quiz = root.Quiz.FirstOrDefault();
        if (quiz != null)
        {
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
        for (int i = 0; i < questionList.Count; i++)
        {
            int randomIndex = random.Next(0, questionList.Count);
            QuestionsRandomized.Add(questionList[randomIndex]);
            questionList.RemoveAt(randomIndex);
        }
    }
}