using QuizBox.Model;
using System.Text.Json;

namespace QuizBox;

[QueryProperty(nameof(Path), "param")]
public partial class QuizStartPage : ContentPage
{
	public string? Path { get; set; }
    private int QNo = 0;

    public QuizStartPage()
	{
		InitializeComponent();
	}

	private void onQNo6(object sender, EventArgs e)
	{

	}
    private void onQNo10(object sender, EventArgs e)
    {

    }
    private void onQNo15(object sender, EventArgs e)
    {

    }
    private void onQNo20(object sender, EventArgs e)
    {

    }

    private void VerticalStackLayout_Loaded(object sender, EventArgs e)
    {
        string jsonString = File.ReadAllText(Path);
        Root root = JsonSerializer.Deserialize<Root>(jsonString);

        Quiz quiz = root.Quiz.FirstOrDefault();
        if (quiz != null)
        {
            QuizTitle.Text = quiz.Name;
            QuizDesc.Text = quiz.Description;
            foreach (var question in quiz.Questions)
            {
                QNo++;
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
        }

    }
}