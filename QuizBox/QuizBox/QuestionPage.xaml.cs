using QuizBox.Model;

namespace QuizBox;

[QueryProperty(nameof(Path), "paramPath")]
[QueryProperty(nameof(QNo), "param")]
public partial class QuestionPage : ContentPage
{
	public int QNo { get; set; }
    public string? Path { get; set; }
	public List<Question> randomizedQuestions { get; private set; }
    public static int currQuestion = 0;
    public static int correctAnswers = 0;
    public QuestionPage()
	{
		InitializeComponent();
        randomizedQuestions = QuizStartPage.QuestionsRandomized;
    }

    private void onAnswer1_Clicked(object sender, EventArgs e)
    {
        if (randomizedQuestions[currQuestion].Answers[0].IsCorrect)
        {
            correctAnswers++;
        }
        currQuestion++;
        NextQuestion();
    }

    private void onAnswer2_Clicked(object sender, EventArgs e)
    {
        if (randomizedQuestions[currQuestion].Answers[1].IsCorrect)
        {
            correctAnswers++;
        }
        currQuestion++;
        NextQuestion();
    }

    private void onAnswer3_Clicked(object sender, EventArgs e)
    {
        if (randomizedQuestions[currQuestion].Answers[2].IsCorrect)
        {
            correctAnswers++;
        }
        currQuestion++;
        NextQuestion();
    }

    private void onAnswer4_Clicked(object sender, EventArgs e)
    {
        if (randomizedQuestions[currQuestion].Answers[3].IsCorrect)
        {
            correctAnswers++;
        }
        currQuestion++;
        NextQuestion();    
    }

    private void VerticalStackLayout_Loaded(object sender, EventArgs e)
    {
        if (currQuestion != 0)
        {
            currQuestion = 0;
            correctAnswers = 0;
        }
        QuestionLabel.Text = randomizedQuestions[currQuestion].QuestionText;
        Answer1.Text = randomizedQuestions[currQuestion].Answers[0].AnswerText;
        Answer2.Text = randomizedQuestions[currQuestion].Answers[1].AnswerText;
        Answer3.Text = randomizedQuestions[currQuestion].Answers[2].AnswerText;
        Answer4.Text = randomizedQuestions[currQuestion].Answers[3].AnswerText;
    }

    private async void NextQuestion()
    {
        if (currQuestion < QNo)
        {   
            QuestionLabel.Text = randomizedQuestions[currQuestion].QuestionText;
            Answer1.Text = randomizedQuestions[currQuestion].Answers[0].AnswerText;
            Answer2.Text = randomizedQuestions[currQuestion].Answers[1].AnswerText;
            Answer3.Text = randomizedQuestions[currQuestion].Answers[2].AnswerText;
            Answer4.Text = randomizedQuestions[currQuestion].Answers[3].AnswerText;
        }
        else
        {
            await Shell.Current.GoToAsync($"///ResultPage?param={correctAnswers}&paramPath={Path}&QuestionParam={QNo}");
        }
    }
}