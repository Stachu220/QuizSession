namespace QuizBox;

[QueryProperty(nameof(Path), "param")]
public partial class QuestionCreatorPage : ContentPage
{
    public string? Path { get; set; }
    private string? QuestionImage;
    private string? AnswerImage1;
    private string? AnswerImage2;
    private string? AnswerImage3;
    private string? AnswerImage4;

    public QuestionCreatorPage()
	{
		InitializeComponent();
    }

    private void onAddQuestionImage(object sender, EventArgs e)
    {
        QuestionImage = ImageToBase64("path");
        AddAnswerImage1.Text = QuestionImage;
    }

    private void onAddAnswerImage1(object sender, EventArgs e)
    {
        AnswerImage1 = ImageToBase64("path");
        AddAnswerImage1.Text = AnswerImage1;
    }

    private void onAddAnswerImage2(object sender, EventArgs e)
    {
        AnswerImage2 = ImageToBase64("path");
        AddAnswerImage2.Text = AnswerImage2;
    }

    private void onAddAnswerImage3(object sender, EventArgs e)
    {
        AnswerImage3 = ImageToBase64("path");
        AddAnswerImage3.Text = AnswerImage3;
    }

    private void onAddAnswerImage4(object sender, EventArgs e)
    {
        AnswerImage4 = ImageToBase64("path");
        AddAnswerImage4.Text = AnswerImage4;
    }

    private void onPrevQuestionClicked(object sender, EventArgs e)
    {
        //save questuion to json file then go to
        //the same page, but with data from the question with number one lower
        //if the question number is 0, go to the quiz creator page

        
    }

    private void onNextQuestionClicked(object sender, EventArgs e)
    {
        //save questions to json file then go to
        //the same page but resseted, prolly with incremented question number

    }

    private void onFinishQuizClicked(object sender, EventArgs e)
    {
        //save question to json file then go to
        //main page
    }

    private string ImageToBase64(string imagePath)
    {
        // Convert the image to a Base64 string
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        return base64String;
    }
}