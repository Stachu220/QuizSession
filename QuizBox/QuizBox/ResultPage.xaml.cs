using QuizBox.Model;

namespace QuizBox;

[QueryProperty(nameof(Path), "paramPath")]
[QueryProperty(nameof(CorrectAnswers), "param")]
[QueryProperty(nameof(QNo), "QuestionParam")]
public partial class ResultPage : ContentPage
{
    public int QNo { get; set; }
    public string? Path { get; set; }
    public int CorrectAnswers { get; set; }
    public List<Question> randomizedQuestions { get; private set; }

    public ResultPage()
	{
		InitializeComponent();
        randomizedQuestions = QuizStartPage.QuestionsRandomized;
    }

    private void CorrectAnswer_Loaded(object sender, EventArgs e)
    {
        // Display the number of correct answers
        CorrectNo.Text = $"You got {CorrectAnswers} out of {QNo} questions correct!";

        for (int i = 0; i < QNo; i++)
        {
            //add questions and correct answers to the stacklayout
            var question = randomizedQuestions[i];
            var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
            if (correctAnswer != null)
            {
                var questionLabel = new Label
                {
                    Text = $"Question {i + 1}: {question.QuestionText}",
                    FontSize = 20,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                var correctAnswerLabel = new Label
                {
                    Text = $"Correct Answer: {correctAnswer.AnswerText}",
                    FontSize = 18,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.CenterAndExpand
                };

                CorrectAnswersStackLayout.Children.Add(questionLabel);
                CorrectAnswersStackLayout.Children.Add(correctAnswerLabel);
            }
        }

    }

    private void onGoBack(object sender, EventArgs e)
    {
        // Go back to the previous page
        Shell.Current.GoToAsync($"///QuizListPage");
    }
}