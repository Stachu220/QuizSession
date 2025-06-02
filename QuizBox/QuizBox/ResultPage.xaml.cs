using Microsoft.Maui.Controls.Shapes;
using QuizBox.Model;
using QuizBox.Helpers;

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

    protected async override void OnAppearing()
    {

        trophyIcon.Opacity = 0;
        trophyIcon.Scale = 0.5;

        var trophyIconAnim = Task.WhenAll(
            trophyIcon.FadeTo(1, 1250, Easing.CubicIn),
            trophyIcon.ScaleTo(1.0, 1250, Easing.CubicOut)
        );

        await Task.WhenAll(trophyIconAnim);
    }

    private void CorrectAnswer_Loaded(object sender, EventArgs e)
    {
        CorrectAnswersStackLayout.Children.Clear();
        CorrectNo.Text = $"You got {CorrectAnswers} out of {QNo} questions correct!";

        for (int i = 0; i < QNo; i++)
        {
            var question = randomizedQuestions[i];
            var selectedAnswer = QuizStartPage.AnsweredQuestions
                .FirstOrDefault(aq => aq.Question == question)?.SelectedAnswer;

            var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);

            var iconLabel = new Label
            {
                FontFamily = "MaterialDesignIcons",
                FontSize = 24,
                Text = selectedAnswer == correctAnswer ? MaterialDesignIconsFonts.CheckCircleOutline : MaterialDesignIconsFonts.CloseCircleOutline,
                TextColor = Colors.White,
                Margin = new Thickness(0, 0, 0, 5),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            var questionLabel = new Label
            {
                Text = $"Question {i + 1}: {question.QuestionText}",
                FontSize = 16,
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                FontFamily = "Ubuntu-Bold"
            };

            var selectedLabel = new Label
            {
                Text = $"Your Answer: {(selectedAnswer != null ? selectedAnswer.AnswerText : "No answer selected")}",
                FontSize = 14,
                TextColor = selectedAnswer == correctAnswer ? Colors.LightGreen : Colors.OrangeRed,
                FontFamily = "Ubuntu-Regular"
            };

            var correctLabel = new Label
            {
                Text = selectedAnswer == correctAnswer ? "" : $"Correct Answer: {correctAnswer?.AnswerText}",
                FontSize = 14,
                TextColor = Colors.LightGreen,
                FontFamily = "Ubuntu-Regular"
            };

            var border = new Border
            {
                BackgroundColor = selectedAnswer == correctAnswer ? Color.FromArgb("#226622") : Color.FromArgb("#662222"),
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
                    Children = { iconLabel, questionLabel, selectedLabel }
                }
            };

            if (selectedAnswer != correctAnswer)
                ((VerticalStackLayout)border.Content).Children.Add(correctLabel);

            CorrectAnswersStackLayout.Children.Add(border);
        }

        QuizStartPage.AnsweredQuestions.Clear();
    }


    private async void onGoBack(object sender, EventArgs e)
    {
        goBackFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        goBackFrame.Background = (Color)Application.Current.Resources["Cerulean"];

        await goBackFrame.ScaleTo(0.9, 180, Easing.CubicIn);
        await goBackFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        goBackFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        goBackFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

        await Shell.Current.GoToAsync($"///MainPage");
    }
}