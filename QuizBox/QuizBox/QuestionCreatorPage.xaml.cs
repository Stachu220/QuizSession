using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using QuizBox.Model;
using System.Text.Json;
using SkiaSharp;

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
    private int QNo;
    private int LatestQNo;
    private bool isThereCorrectAnswer = false;

    public QuestionCreatorPage()
	{
		InitializeComponent();
    }

    private void onStackLayout_loaded(object sender, EventArgs e)
    {
        QNo = 0;
        LatestQNo = 0;
        AnswerEntry1.Text = "";
        AnswerEntry2.Text = "";
        AnswerEntry3.Text = "";
        AnswerEntry4.Text = "";
        QuestionEntry.Text = "";
        CorrectAnswer1.IsChecked = false;
        CorrectAnswer2.IsChecked = false;
        CorrectAnswer3.IsChecked = false;
        CorrectAnswer4.IsChecked = false;
    }

    //Somehow retrieve the image path from the file picker
    private async void onAddQuestionImage(object sender, EventArgs e)
    {
        QuestionImage = await ImageToBase64();
        QuestionEntry.Text = QuestionImage;
    }

    private async void onAddAnswerImage1(object sender, EventArgs e)
    {
        AnswerImage1 = await ImageToBase64();
        AnswerEntry1.Text = AnswerImage1;
    }

    private async void onAddAnswerImage2(object sender, EventArgs e)
    {
        AnswerImage2 = await ImageToBase64();
        AnswerEntry2.Text = AnswerImage2;
    }

    private async void onAddAnswerImage3(object sender, EventArgs e)
    {
        AnswerImage3 = await ImageToBase64();
        AnswerEntry3.Text = AnswerImage3;
    }

    private async void onAddAnswerImage4(object sender, EventArgs e)
    {
        AnswerImage4 = await ImageToBase64();
        AnswerEntry4.Text = AnswerImage4;
    }

    private async void onPrevQuestionClicked(object sender, EventArgs e)
    {
        if (QNo <= 0)
        {
            await DisplayAlert("Info", "To jest pierwsze pytanie.", "OK");
            return;
        }

        if (!string.IsNullOrEmpty(QuestionEntry.Text))
        {
            if (QNo == LatestQNo)
                compileQuestion();
            else
                updateQuestion();
        }

        QNo--;

        string json = File.ReadAllText(Path);
        var quizData = JsonSerializer.Deserialize<Root>(json);
        var quiz = quizData.Quiz.FirstOrDefault();
        if (quiz != null)
        {
            var question = quiz.Questions.FirstOrDefault(q => q.QuestionID == QNo.ToString());
            if (question != null)
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

    private async void onNextQuestionClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(QuestionEntry.Text))
        {
            await DisplayAlert("Error", "Please fill in the question text.", "OK");
            return;
        }

        if (QNo == LatestQNo)
        {
            compileQuestion();
        }
        else
        {
            updateQuestion();
        }

        QNo++;

        string json = File.ReadAllText(Path);
        var quizData = JsonSerializer.Deserialize<Root>(json);
        var quiz = quizData.Quiz.FirstOrDefault();
        var question = quiz?.Questions.FirstOrDefault(q => q.QuestionID == QNo.ToString());

        if (question != null)
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
        else
        {
            // Nowe pytanie - wyczyść pola
            QuestionEntry.Text = "";
            AnswerEntry1.Text = "";
            AnswerEntry2.Text = "";
            AnswerEntry3.Text = "";
            AnswerEntry4.Text = "";
            CorrectAnswer1.IsChecked = false;
            CorrectAnswer2.IsChecked = false;
            CorrectAnswer3.IsChecked = false;
            CorrectAnswer4.IsChecked = false;
        }
    }

    private async void onFinishQuizClicked(object sender, EventArgs e)
    {
        //save question to json file then go to
        //main page
        string json = File.ReadAllText(Path);
        var quizData = JsonSerializer.Deserialize<Root>(json);
        var quiz = quizData.Quiz.FirstOrDefault();
        if (!string.IsNullOrEmpty(QuestionEntry.Text))
        {

            var existingQuestion = quiz.Questions.FirstOrDefault(q => q.QuestionID == QNo.ToString());
            if (existingQuestion != null)
            {
                updateQuestion();
            }
            else
            {
                compileQuestion();
            }
            await Toast.Make("Quiz was created successfully!", ToastDuration.Short).Show();
        }
        else if (quiz.Questions.Count == 0)
        {
            File.Delete(Path);
            await DisplayAlert(quiz.Name, "Quiz not created, can't create empty quiz", "OK");
        }

        await Shell.Current.GoToAsync($"///MainPage");
    }

    private async Task<string> ImageToBase64()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Select an image",
            FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.Android, new[] { "image/*" } },
                { DevicePlatform.iOS, new[] { "public.image" } },
                { DevicePlatform.WinUI, new[] { ".jpg", ".jpeg", ".png", ".gif" } }
            })
        });

        if (result != null)
        {
            try
            {
                using var inputStream = await result.OpenReadAsync();
                using var original = SKBitmap.Decode(inputStream);

                if (original == null)
                    return string.Empty;

                float widthRatio = 512f / original.Width;
                float heightRatio = 512f / original.Height;
                float scale = Math.Min(widthRatio, heightRatio);

                int newWidth = (int)(original.Width * scale);
                int newHeight = (int)(original.Height * scale);

                using var resized = original.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Medium);

                using var image = SKImage.FromBitmap(resized);
                using var data = image.Encode(SKEncodedImageFormat.Jpeg, 60);

                return Convert.ToBase64String(data.ToArray());


            } catch (Exception e)
            {
                Console.WriteLine($"Error processing image: {e.Message}");
                return string.Empty;
            }
        }

        return string.Empty;
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
        foreach (var answer in question.Answers)
        {
            if (answer.IsCorrect)
            {
                LatestQNo++;
                isThereCorrectAnswer = true;
                break;
            }
        }
        if (isThereCorrectAnswer)
        {
            pushToJson(question);
        }
        else
        {
            DisplayAlert("Error", "Please select at least one correct answer.", "OK");
        }
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

    private void correctAnswer_CheckedChanged(object sender, CheckedChangedEventArgs e)
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

    private void updateQuestion()
    {
        string json = File.ReadAllText(Path);
        var quizData = JsonSerializer.Deserialize<Root>(json);
        var quiz = quizData.Quiz.FirstOrDefault();

        var existingQuestion = quiz.Questions.FirstOrDefault(q => q.QuestionID == QNo.ToString());
        if (existingQuestion != null)
        {
            existingQuestion.QuestionText = QuestionEntry.Text;
            existingQuestion.Answers[0].AnswerText = AnswerEntry1.Text;
            existingQuestion.Answers[0].IsCorrect = CorrectAnswer1.IsChecked == true;
            existingQuestion.Answers[1].AnswerText = AnswerEntry2.Text;
            existingQuestion.Answers[1].IsCorrect = CorrectAnswer2.IsChecked == true;
            existingQuestion.Answers[2].AnswerText = AnswerEntry3.Text;
            existingQuestion.Answers[2].IsCorrect = CorrectAnswer3.IsChecked == true;
            existingQuestion.Answers[3].AnswerText = AnswerEntry4.Text;
            existingQuestion.Answers[3].IsCorrect = CorrectAnswer4.IsChecked == true;
        }

        string updatedJson = JsonSerializer.Serialize(quizData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(Path, updatedJson);
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Cancel", "Would you like to cancel the creation of quiz?", "Yes", "No");
        if (answer)
        {
            AnswerEntry1.Text = "";
            AnswerEntry2.Text = "";
            AnswerEntry3.Text = "";
            AnswerEntry4.Text = "";
            QuestionEntry.Text = "";

            CorrectAnswer1.IsChecked = false;
            CorrectAnswer2.IsChecked = false;
            CorrectAnswer3.IsChecked = false;
            CorrectAnswer4.IsChecked = false;

            File.Delete(Path);
            await Shell.Current.GoToAsync("///MainPage");
            await Toast.Make("Canceling the creation of quiz!", ToastDuration.Short).Show();
        }
    }
}