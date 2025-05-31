using Microsoft.Maui.Controls;
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
    private int imageContainer = 0;
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
            imageContainer = 0;
        }
        if (!CheckForImage(randomizedQuestions[currQuestion].QuestionText))
            QuestionLabel.Text = randomizedQuestions[currQuestion].QuestionText;
        imageContainer++;
        if (!CheckForImage(randomizedQuestions[currQuestion].Answers[0].AnswerText))
            Answer1.Text = randomizedQuestions[currQuestion].Answers[0].AnswerText;
        imageContainer++;
        if (!CheckForImage(randomizedQuestions[currQuestion].Answers[1].AnswerText))
            Answer2.Text = randomizedQuestions[currQuestion].Answers[1].AnswerText;
        imageContainer++;
        if (!CheckForImage(randomizedQuestions[currQuestion].Answers[2].AnswerText))
            Answer3.Text = randomizedQuestions[currQuestion].Answers[2].AnswerText;
        imageContainer++;
        if (!CheckForImage(randomizedQuestions[currQuestion].Answers[3].AnswerText))
            Answer4.Text = randomizedQuestions[currQuestion].Answers[3].AnswerText;
    }

    private async void NextQuestion()
    {
        if (currQuestion < QNo)
        {
            imageContainer = 0;
            if (!CheckForImage(randomizedQuestions[currQuestion].QuestionText))
                QuestionLabel.Text = randomizedQuestions[currQuestion].QuestionText;
            imageContainer++;
            if (!CheckForImage(randomizedQuestions[currQuestion].Answers[0].AnswerText))
                Answer1.Text = randomizedQuestions[currQuestion].Answers[0].AnswerText;
            imageContainer++;
            if (!CheckForImage(randomizedQuestions[currQuestion].Answers[1].AnswerText))
                Answer2.Text = randomizedQuestions[currQuestion].Answers[1].AnswerText;
            imageContainer++;
            if (!CheckForImage(randomizedQuestions[currQuestion].Answers[2].AnswerText))
                Answer3.Text = randomizedQuestions[currQuestion].Answers[2].AnswerText;
            imageContainer++;
            if (!CheckForImage(randomizedQuestions[currQuestion].Answers[3].AnswerText))
                Answer4.Text = randomizedQuestions[currQuestion].Answers[3].AnswerText;
        }
        else
        {
            await Shell.Current.GoToAsync($"///ResultPage?param={correctAnswers}&paramPath={Path}&QuestionParam={QNo}");
        }
    }

    private void base64ToImage(string base64String)
    {
        if (string.IsNullOrEmpty(base64String)) return;
        try
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            switch (imageContainer)
            {
                case 0:
                    QuestionImage.Source = imageSource;
                    break;
                case 1:
                    //Answer1Image.Source = imageSource;
                    break;
                case 2:
                    //Answer2Image.Source = imageSource;
                    break;
                case 3:
                    //Answer3Image.Source = imageSource;
                    break;
                case 4:
                    //Answer4Image.Source = imageSource;
                    break;
            }
        }
        catch (FormatException)
        {
            // Handle the case where the base64 string is not valid
            //QuestionImage.Source = null; // or set a default image
        }
    }

    private bool CheckForImage(string str)
    {
        if (string.IsNullOrEmpty(str)) return false;

        string firstFourSymbols = str.Length >= 4 ? str.Substring(0, 4) : str;
        if (firstFourSymbols == "/9j/")
        {
            base64ToImage(str);
            return true;
        }

        return false;
    }
}