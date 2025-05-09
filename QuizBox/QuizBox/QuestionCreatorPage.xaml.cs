using QuizBox.Model;
using System.Text.Json;

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
    private int QNo = 0;

    public QuestionCreatorPage()
	{
		InitializeComponent();
    }

    private void onAddQuestionImage(object sender, EventArgs e)
    {
        QuestionImage = ImageToBase64("path");
        AddAnswerImage1.Text = QuestionImage;
    }

    //Somehow retrieve the image path from the file picker
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

    private async void onPrevQuestionClicked(object sender, EventArgs e)
    {
        //save questuion to json file then go to
        //the same page, but with data from the question with number one lower
        //if the question number is 0, go to the quiz creator page
        compileQuestion();
        QNo--;
        if (QNo < 0)
        {
            QNo = 0;
            await Shell.Current.GoToAsync($"///QuizCreatorPage");
        }
        else
        {
            string json = File.ReadAllText(Path);
            var quizData = JsonSerializer.Deserialize<Root>(json);
            var quiz = quizData.Quiz.FirstOrDefault();
            if (quiz != null)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.QuestionID == QNo.ToString());
                if(question != null)
                {
                    QuestionEntry.Text = question.QuestionText;
                    AnswerEntry1.Text = question.Answers[0].AnswerText;
                    CorrectAnswer1.IsChecked = question.Answers[0].IsCorrect;
                    AnswerEntry2.Text = question.Answers[1].AnswerText;
                    CorrectAnswer2.IsChecked = question.Answers[1].IsCorrect;
                    AnswerEntry3.Text = question.Answers[2].AnswerText;
                    CorrectAnswer3.IsChecked = question.Answers[2].IsCorrect;
                    AnswerEntry4.Text = question.Answers[3].AnswerText;
                    CorrectAnswer4.IsChecked = question.Answers[3].IsCorrect;
                }
            }
        }

    }

    private void onNextQuestionClicked(object sender, EventArgs e)
    {
        //save questions to json file then go to
        //the same page but resseted, prolly with incremented question number
        compileQuestion();
        AnswerEntry1.Text = "";
        AnswerEntry2.Text = "";
        AnswerEntry3.Text = "";
        AnswerEntry4.Text = "";
        QuestionEntry.Text = "";
        CorrectAnswer1.IsChecked = false;
        CorrectAnswer2.IsChecked = false;
        CorrectAnswer3.IsChecked = false;
        CorrectAnswer4.IsChecked = false;
        QNo++;
    }

    private async void onFinishQuizClicked(object sender, EventArgs e)
    {
        //save question to json file then go to
        //main page
        compileQuestion();
        await Shell.Current.GoToAsync($"///MainPage");
    }

    private string ImageToBase64(string imagePath)
    {
        // Convert the image to a Base64 string
        byte[] imageBytes = File.ReadAllBytes(imagePath);
        string base64String = Convert.ToBase64String(imageBytes);
        return base64String;
    }

    private void compileQuestion()
    {
        var question = new Question
        {
            QuestionID = QNo.ToString(),
            QuestionText = QuestionEntry.Text,
            Answers = new List<Answer>
            {
                new Answer { AnswerText = AnswerEntry1.Text, IsCorrect = CorrectAnswer1.IsChecked == true },
                new Answer { AnswerText = AnswerEntry2.Text, IsCorrect = CorrectAnswer2.IsChecked == true },
                new Answer { AnswerText = AnswerEntry3.Text, IsCorrect = CorrectAnswer3.IsChecked == true },
                new Answer { AnswerText = AnswerEntry4.Text, IsCorrect = CorrectAnswer4.IsChecked == true }
            }
        };

        pushToJson(question);
    }

    private async void pushToJson(Question newQuestion)
    {
        if (File.Exists(Path))
        {
            string json = File.ReadAllText(Path);

            var quizData = JsonSerializer.Deserialize<Root>(json);

            var quiz = quizData.Quiz.FirstOrDefault();

            quiz.Questions.Add(newQuestion);

            string updatedJson = JsonSerializer.Serialize(quizData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(Path, updatedJson);
        }
        else
        {
            await DisplayAlert("Error", "Can't find the quiz file. Please try again.", "OK");
        }
    }

    private void CorrectAnswer_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var targetButton = sender as CheckBox;
        if (targetButton.IsChecked)
        {
            foreach (var radioButton in new[] { CorrectAnswer1, CorrectAnswer2, CorrectAnswer3, CorrectAnswer4 })
            {
                if (radioButton != targetButton)
                {
                    radioButton.IsChecked = false;
                }
            }
        }
    }
}