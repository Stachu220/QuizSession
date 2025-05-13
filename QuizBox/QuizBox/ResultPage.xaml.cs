using Microsoft.Maui.Controls.Shapes;
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
        CorrectAnswersStackLayout.Children.Clear(); // Clear previous content if any
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
                    FontSize = 16,
                    TextColor = Colors.White,
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center
                };

                var correctAnswerLabel = new Label
                {
                    Text = $"Correct Answer: {correctAnswer.AnswerText}",
                    FontSize = 14,
                    TextColor = Colors.White,
                    HorizontalOptions = LayoutOptions.Start,
                    VerticalOptions = LayoutOptions.Center
                };

                var border = new Border
                {
                    BackgroundColor = Color.FromHex("#005f80"),
                    StrokeThickness = 0,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(12)
                    },
                    Margin = new Thickness(0, 0, 0, 12),
                    Padding = new Thickness(16, 12),
                    Content = new VerticalStackLayout
                    {
                        Spacing = 2,
                        Children = { questionLabel, correctAnswerLabel }
                    }
                };

                CorrectAnswersStackLayout.Children.Add(border);
            }
        }

    }

    private void onGoBack(object sender, EventArgs e)
    {
        // Go back to the previous page
        Shell.Current.GoToAsync($"///QuizListPage");
    }
}