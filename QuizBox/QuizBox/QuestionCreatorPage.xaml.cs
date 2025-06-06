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

        QuestionImage = null;
        QuestionEntry.Text = "";

        AnswerEntry1.Text = "";
        AnswerEntry2.Text = "";
        AnswerEntry3.Text = "";
        AnswerEntry4.Text = "";

        AnswerImage1 = null;
        AnswerImage2 = null;
        AnswerImage3 = null;
        AnswerImage4 = null;

        CorrectAnswer1.IsChecked = false;
        CorrectAnswer2.IsChecked = false;
        CorrectAnswer3.IsChecked = false;
        CorrectAnswer4.IsChecked = false;

        addImageBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
        icon.TextColor = (Color)Application.Current.Resources["PictonBlue"];
        text1.TextColor = (Color)Application.Current.Resources["PictonBlue"];

        answerImage1Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
        answerImage1Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

        answerImage2Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
        answerImage2Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

        answerImage3Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
        answerImage3Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

        answerImage4Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
        answerImage4Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
    }

    //Somehow retrieve the image path from the file picker
    private async void onAddQuestionImage(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(QuestionImage))
        {
            addImageBorder.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            icon.TextColor = (Color)Application.Current.Resources["Cerulean"];
            text1.TextColor = (Color)Application.Current.Resources["Cerulean"];
        }
        else
        {
            addImageBorder.Stroke = Color.FromHex("#45A049");
            icon.TextColor = Color.FromHex("#45A049");
            text1.TextColor = Color.FromHex("#45A049");
        }

        await addImageBorder.ScaleTo(0.9, 180, Easing.CubicIn);
        await addImageBorder.ScaleTo(1.0, 180, Easing.CubicOut);

        QuestionImage = await ImageToBase64();

        if(String.IsNullOrEmpty(QuestionImage))
        {
            addImageBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            icon.TextColor = (Color)Application.Current.Resources["PictonBlue"];
            text1.TextColor = (Color)Application.Current.Resources["PictonBlue"];
        }
        else
        {
            addImageBorder.Stroke = Color.FromHex("#4CAF50");
            icon.TextColor = Color.FromHex("#4CAF50");
            text1.TextColor = Color.FromHex("#4CAF50");
        }
    }

    private async void onAddAnswerImage1(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(AnswerImage1))
        {
            answerImage1Frame.Background = (Color)Application.Current.Resources["Cerulean"];
            answerImage1Frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        }
        else
        {
            answerImage1Frame.Background = Color.FromHex("#45A049");
            answerImage1Frame.BackgroundColor = Color.FromHex("#45A049");
        }

        await answerImage1Frame.ScaleTo(0.85, 180, Easing.CubicIn);
        await answerImage1Frame.ScaleTo(1.0, 180, Easing.CubicOut);

        AnswerImage1 = await ImageToBase64();

        if (String.IsNullOrEmpty(AnswerImage1))
        {
            answerImage1Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage1Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        }
        else
        {
            answerImage1Frame.Background = Color.FromHex("#4CAF50");
            answerImage1Frame.BackgroundColor = Color.FromHex("#4CAF50");
        }
    }

    private async void onAddAnswerImage2(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(AnswerImage2))
        {
            answerImage2Frame.Background = (Color)Application.Current.Resources["Cerulean"];
            answerImage2Frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        }
        else
        {
            answerImage2Frame.Background = Color.FromHex("#45A049");
            answerImage2Frame.BackgroundColor = Color.FromHex("#45A049");
        }

        await answerImage2Frame.ScaleTo(0.85, 180, Easing.CubicIn);
        await answerImage2Frame.ScaleTo(1.0, 180, Easing.CubicOut);

        AnswerImage2 = await ImageToBase64();

        if (String.IsNullOrEmpty(AnswerImage2))
        {
            answerImage2Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage2Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        }
        else
        {
            answerImage2Frame.Background = Color.FromHex("#4CAF50");
            answerImage2Frame.BackgroundColor = Color.FromHex("#4CAF50");
        }
    }

    private async void onAddAnswerImage3(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(AnswerImage3))
        {
            answerImage3Frame.Background = (Color)Application.Current.Resources["Cerulean"];
            answerImage3Frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        }
        else
        {
            answerImage3Frame.Background = Color.FromHex("#45A049");
            answerImage3Frame.BackgroundColor = Color.FromHex("#45A049");
        }

        await answerImage3Frame.ScaleTo(0.85, 180, Easing.CubicIn);
        await answerImage3Frame.ScaleTo(1.0, 180, Easing.CubicOut);

        AnswerImage3 = await ImageToBase64();

        if (String.IsNullOrEmpty(AnswerImage3))
        {
            answerImage3Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage3Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        }
        else
        {
            answerImage3Frame.Background = Color.FromHex("#4CAF50");
            answerImage3Frame.BackgroundColor = Color.FromHex("#4CAF50");
        }
    }

    private async void onAddAnswerImage4(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(AnswerImage4))
        {
            answerImage4Frame.Background = (Color)Application.Current.Resources["Cerulean"];
            answerImage4Frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        }
        else
        {
            answerImage4Frame.Background = Color.FromHex("#45A049");
            answerImage4Frame.BackgroundColor = Color.FromHex("#45A049");
        }

        await answerImage4Frame.ScaleTo(0.85, 180, Easing.CubicIn);
        await answerImage4Frame.ScaleTo(1.0, 180, Easing.CubicOut);

        AnswerImage4 = await ImageToBase64();

        if (String.IsNullOrEmpty(AnswerImage4))
        {
            answerImage4Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage4Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        }
        else
        {
            answerImage4Frame.Background = Color.FromHex("#4CAF50");
            answerImage4Frame.BackgroundColor = Color.FromHex("#4CAF50");
        }
    }

    private async void onPrevQuestionClicked(object sender, EventArgs e)
    {
        previousFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        previousFrame.Background = (Color)Application.Current.Resources["Cerulean"];

        await previousFrame.ScaleTo(0.85, 180, Easing.CubicIn);
        await previousFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        previousFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        previousFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

        if (QNo <= 0)
        {
            await DisplayAlert("INFO", "This is first question!", "OK");
            return;
        }

        if (!string.IsNullOrEmpty(QuestionEntry.Text))
        {

            try
            {
                var answers = GetCurrentAnswers();

                if (QNo == LatestQNo)
                    compileQuestion();
                else
                    updateQuestion();
            }
            catch (Exception ex)
            {
                if (ex.Message == "not_enough_answers")
                {
                    await DisplayAlert("ERROR", "You must provide at least two answers (text or image)!", "OK");
                    return;
                }
                else if (ex.Message == "no_correct_answer")
                {
                    await DisplayAlert("ERROR", "Please select at least one correct answer!", "OK");
                    return;
                }
                else if (ex.Message == "images_error")
                {
                    await DisplayAlert("ERROR", "All answers must have images or none of them!", "OK");
                    return;
                }
            }
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
                QuestionImage = question.QuestionImage;

                for(int i=0;i< question.Answers.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            AnswerEntry1.Text = question.Answers[0].AnswerText;
                            AnswerImage1 = question.Answers[0].AnswerImage;
                            CorrectAnswer1.IsChecked = question.Answers[0].IsCorrect;
                            break;
                        case 1:
                            AnswerEntry2.Text = question.Answers[1].AnswerText;
                            AnswerImage2 = question.Answers[1].AnswerImage;
                            CorrectAnswer2.IsChecked = question.Answers[1].IsCorrect;
                            break;
                        case 2:
                            AnswerEntry3.Text = question.Answers[2].AnswerText;
                            AnswerImage3 = question.Answers[2].AnswerImage;
                            CorrectAnswer3.IsChecked = question.Answers[2].IsCorrect;
                            break;
                        case 3:
                            AnswerEntry4.Text = question.Answers[3].AnswerText;
                            AnswerImage4 = question.Answers[3].AnswerImage;
                            CorrectAnswer4.IsChecked = question.Answers[3].IsCorrect;
                            break;
                    }
                }

                if(String.IsNullOrEmpty(QuestionImage))
                {
                    addImageBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
                    icon.TextColor = (Color)Application.Current.Resources["PictonBlue"];
                    text1.TextColor = (Color)Application.Current.Resources["PictonBlue"];
                }
                else
                {
                    addImageBorder.Stroke = Color.FromHex("#4CAF50");
                    icon.TextColor = Color.FromHex("#4CAF50");
                    text1.TextColor = Color.FromHex("#4CAF50");
                }

                if (String.IsNullOrEmpty(AnswerImage1))
                {
                    answerImage1Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                    answerImage1Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
                }
                else
                {
                    answerImage1Frame.Background = Color.FromHex("#4CAF50");
                    answerImage1Frame.BackgroundColor = Color.FromHex("#4CAF50");
                }

                if (String.IsNullOrEmpty(AnswerImage2))
                {
                    answerImage2Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                    answerImage2Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
                }
                else
                {
                    answerImage2Frame.Background = Color.FromHex("#4CAF50");
                    answerImage2Frame.BackgroundColor = Color.FromHex("#4CAF50");
                }

                if (String.IsNullOrEmpty(AnswerImage3))
                {
                    answerImage3Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                    answerImage3Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
                }
                else
                {
                    answerImage3Frame.Background = Color.FromHex("#4CAF50");
                    answerImage3Frame.BackgroundColor = Color.FromHex("#4CAF50");
                }

                if (String.IsNullOrEmpty(AnswerImage4))
                {
                    answerImage4Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                    answerImage4Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
                }
                else
                {
                    answerImage4Frame.Background = Color.FromHex("#4CAF50");
                    answerImage4Frame.BackgroundColor = Color.FromHex("#4CAF50");
                }
            }
        }
    }

    private async void onNextQuestionClicked(object sender, EventArgs e)
    {
        nextFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        nextFrame.Background = (Color)Application.Current.Resources["Cerulean"];

        await nextFrame.ScaleTo(0.85, 180, Easing.CubicIn);
        await nextFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        nextFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        nextFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

        if (string.IsNullOrEmpty(QuestionEntry.Text))
        {
            await DisplayAlert("ERROR", "Please fill in the question text!", "OK");
            return;
        }

        try
        {
            var answers = GetCurrentAnswers();
        }
        catch (Exception ex)
        {
            if(ex.Message == "not_enough_answers")
            {
                await DisplayAlert("ERROR", "You must provide at least two answers (text or image)!", "OK");
                return;
            }
            else if (ex.Message == "no_correct_answer")
            {
                await DisplayAlert("ERROR", "Please select at least one correct answer!", "OK");
                return;
            }
            else if (ex.Message == "images_error")
            {
                await DisplayAlert("ERROR", "All answers must have images or none of them!", "OK");
                return;
            }
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
            QuestionImage = question.QuestionImage;

            for(int i=0; i<question.Answers.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        AnswerEntry1.Text = question.Answers[0].AnswerText;
                        AnswerImage1 = question.Answers[0].AnswerImage;
                        CorrectAnswer1.IsChecked = question.Answers[0].IsCorrect;
                        break;
                    case 1:
                        AnswerEntry2.Text = question.Answers[1].AnswerText;
                        AnswerImage2 = question.Answers[1].AnswerImage;
                        CorrectAnswer2.IsChecked = question.Answers[1].IsCorrect;
                        break;
                    case 2:
                        AnswerEntry3.Text = question.Answers[2].AnswerText;
                        AnswerImage3 = question.Answers[2].AnswerImage;
                        CorrectAnswer3.IsChecked = question.Answers[2].IsCorrect;
                        break;
                    case 3:
                        AnswerEntry4.Text = question.Answers[3].AnswerText;
                        AnswerImage4 = question.Answers[3].AnswerImage;
                        CorrectAnswer4.IsChecked = question.Answers[3].IsCorrect;
                        break;
                }
            }

            if (String.IsNullOrEmpty(QuestionImage))
            {
                addImageBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
                icon.TextColor = (Color)Application.Current.Resources["PictonBlue"];
                text1.TextColor = (Color)Application.Current.Resources["PictonBlue"];
            }
            else
            {
                addImageBorder.Stroke = Color.FromHex("#4CAF50");
                icon.TextColor = Color.FromHex("#4CAF50");
                text1.TextColor = Color.FromHex("#4CAF50");
            }

            if (String.IsNullOrEmpty(AnswerImage1))
            {
                answerImage1Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                answerImage1Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
            }
            else
            {
                answerImage1Frame.Background = Color.FromHex("#4CAF50");
                answerImage1Frame.BackgroundColor = Color.FromHex("#4CAF50");
            }

            if (String.IsNullOrEmpty(AnswerImage2))
            {
                answerImage2Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                answerImage2Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
            }
            else
            {
                answerImage2Frame.Background = Color.FromHex("#4CAF50");
                answerImage2Frame.BackgroundColor = Color.FromHex("#4CAF50");
            }

            if (String.IsNullOrEmpty(AnswerImage3))
            {
                answerImage3Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                answerImage3Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
            }
            else
            {
                answerImage3Frame.Background = Color.FromHex("#4CAF50");
                answerImage3Frame.BackgroundColor = Color.FromHex("#4CAF50");
            }

            if (String.IsNullOrEmpty(AnswerImage4))
            {
                answerImage4Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
                answerImage4Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
            }
            else
            {
                answerImage4Frame.Background = Color.FromHex("#4CAF50");
                answerImage4Frame.BackgroundColor = Color.FromHex("#4CAF50");
            }

        }
        else
        {
            QuestionEntry.Text = "";
            QuestionImage = null;

            AnswerEntry1.Text = "";
            AnswerImage1 = null;

            AnswerEntry2.Text = "";
            AnswerImage2 = null;

            AnswerEntry3.Text = "";
            AnswerImage3 = null;

            AnswerEntry4.Text = "";
            AnswerImage4 = null;

            CorrectAnswer1.IsChecked = false;
            CorrectAnswer2.IsChecked = false;
            CorrectAnswer3.IsChecked = false;
            CorrectAnswer4.IsChecked = false;

            addImageBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            icon.TextColor = (Color)Application.Current.Resources["PictonBlue"];
            text1.TextColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage1Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage1Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage2Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage2Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage3Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage3Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage4Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage4Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        }
    }

    private async void onFinishQuizClicked(object sender, EventArgs e)
    {
        finishFrame.BackgroundColor = Color.FromHex("#45A049");
        finishFrame.Background = Color.FromHex("#45A049");

        await finishFrame.ScaleTo(0.85, 180, Easing.CubicIn);
        await finishFrame.ScaleTo(1.0, 180, Easing.CubicOut);

        finishFrame.BackgroundColor = Color.FromHex("#4CAF50");
        finishFrame.Background = Color.FromHex("#4CAF50");

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
            await DisplayAlert(quiz.Name, "Quiz not created, can't create empty quiz!", "OK");
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

    private List<Answer> GetCurrentAnswers()
    {
        var answers = new List<Answer>();

        if (!string.IsNullOrWhiteSpace(AnswerEntry1.Text) || !string.IsNullOrWhiteSpace(AnswerImage1))
            answers.Add(new Answer
            {
                AnswerText = AnswerEntry1.Text,
                AnswerImage = AnswerImage1,
                IsCorrect = CorrectAnswer1.IsChecked == true
            });

        if (!string.IsNullOrWhiteSpace(AnswerEntry2.Text) || !string.IsNullOrWhiteSpace(AnswerImage2))
            answers.Add(new Answer
            {
                AnswerText = AnswerEntry2.Text,
                AnswerImage = AnswerImage2,
                IsCorrect = CorrectAnswer2.IsChecked == true
            });

        if (!string.IsNullOrWhiteSpace(AnswerEntry3.Text) || !string.IsNullOrWhiteSpace(AnswerImage3))
            answers.Add(new Answer
            {
                AnswerText = AnswerEntry3.Text,
                AnswerImage = AnswerImage3,
                IsCorrect = CorrectAnswer3.IsChecked == true
            });

        if (!string.IsNullOrWhiteSpace(AnswerEntry4.Text) || !string.IsNullOrWhiteSpace(AnswerImage4))
            answers.Add(new Answer
            {
                AnswerText = AnswerEntry4.Text,
                AnswerImage = AnswerImage4,
                IsCorrect = CorrectAnswer4.IsChecked == true
            });

        bool anyHasImage = answers.Any(a => !string.IsNullOrWhiteSpace(a.AnswerImage));
        bool allHaveImage = answers.All(a => !string.IsNullOrWhiteSpace(a.AnswerImage));

        if (answers.Count < 2)
            throw new InvalidOperationException("not_enough_answers");

        if (!answers.Any(a => a.IsCorrect))
            throw new InvalidOperationException("no_correct_answer");

        if (anyHasImage && !allHaveImage)
        {
            throw new InvalidOperationException("images_error");
        }

        return answers;
    }

    private void compileQuestion()
    {
        try
        {
            var question = new Question
            {
                QuestionID = QNo.ToString(),
                QuestionText = QuestionEntry.Text,
                QuestionImage = QuestionImage,
                Answers = GetCurrentAnswers()
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

            pushToJson(question);
        }
        catch (Exception ex)
        {
            if (ex.Message == "not_enough_answers")
            {
                DisplayAlert("ERROR", "You must provide at least two answers (text or image)!", "OK");
            }
            else if (ex.Message == "no_correct_answer")
            {
                DisplayAlert("ERROR", "Please select at least one correct answer!", "OK");
            }
            else if (ex.Message == "images_error")
            {
                DisplayAlert("ERROR", "All answers must have images or none of them!", "OK");
            }
            else
            {
                DisplayAlert("ERROR", ex.Message, "OK");
            }
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
            await DisplayAlert("ERROR", "Can't find the quiz file! Please try again!", "OK");
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
            existingQuestion.QuestionImage = QuestionImage;

            for(int i=0; i < existingQuestion.Answers.Count; i++)
            {
                switch(i)
                {
                    case 0:
                        existingQuestion.Answers[i].AnswerText = AnswerEntry1.Text;
                        existingQuestion.Answers[i].AnswerImage = AnswerImage1;
                        existingQuestion.Answers[i].IsCorrect = CorrectAnswer1.IsChecked == true;
                        break;
                    case 1:
                        existingQuestion.Answers[i].AnswerText = AnswerEntry2.Text;
                        existingQuestion.Answers[i].AnswerImage = AnswerImage2;
                        existingQuestion.Answers[i].IsCorrect = CorrectAnswer2.IsChecked == true;
                        break;
                    case 2:
                        existingQuestion.Answers[i].AnswerText = AnswerEntry3.Text;
                        existingQuestion.Answers[i].AnswerImage = AnswerImage3;
                        existingQuestion.Answers[i].IsCorrect = CorrectAnswer3.IsChecked == true;
                        break;
                    case 3:
                        existingQuestion.Answers[i].AnswerText = AnswerEntry4.Text;
                        existingQuestion.Answers[i].AnswerImage = AnswerImage4;
                        existingQuestion.Answers[i].IsCorrect = CorrectAnswer4.IsChecked == true;
                        break;
                }
            }
        }

        string updatedJson = JsonSerializer.Serialize(quizData, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(Path, updatedJson);
    }

    private async void CancelButton_Clicked(object sender, EventArgs e)
    {
        CancelButton.BackgroundColor = Color.FromHex("#B31900");
        CancelButton.Background = Color.FromHex("#B31900");

        await CancelButton.ScaleTo(0.85, 180, Easing.CubicIn);
        await CancelButton.ScaleTo(1.0, 180, Easing.CubicOut);

        CancelButton.BackgroundColor = Color.FromHex("#FF2400");
        CancelButton.Background = Color.FromHex("#FF2400");

        bool answer = await DisplayAlert("CANCEL", "Would you like to cancel the creation of quiz?", "YES", "NO");
        if (answer)
        {
            AnswerEntry1.Text = "";
            AnswerEntry2.Text = "";
            AnswerEntry3.Text = "";
            AnswerEntry4.Text = "";
            QuestionEntry.Text = "";

            QuestionImage = null;
            AnswerImage1 = null;
            AnswerImage2 = null;
            AnswerImage3 = null;
            AnswerImage4 = null;

            CorrectAnswer1.IsChecked = false;
            CorrectAnswer2.IsChecked = false;
            CorrectAnswer3.IsChecked = false;
            CorrectAnswer4.IsChecked = false;

            addImageBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            icon.TextColor = (Color)Application.Current.Resources["PictonBlue"];
            text1.TextColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage1Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage1Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage2Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage2Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage3Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage3Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

            answerImage4Frame.Background = (Color)Application.Current.Resources["PictonBlue"];
            answerImage4Frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];

            File.Delete(Path);
            await Shell.Current.GoToAsync("///MainPage");
            await Toast.Make("Canceling the creation of quiz!", ToastDuration.Short).Show();
        }
    }

    private async void OnEntryFocused(object sender, FocusEventArgs e)
    {
        if (sender == QuestionEntry)
        {
            QuestionEntryBorder.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            QuestionEntryBorder.StrokeThickness = 4;
            await QuestionEntryBorder.ScaleTo(1.05, 120, Easing.CubicOut);
        }
        else if(sender == AnswerEntry1)
        {
            AnswerEntry1Border.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            AnswerEntry1Border.StrokeThickness = 4;
            await AnswerEntry1Border.ScaleTo(1.05, 120, Easing.CubicOut);

            AnswerEntry4.IsVisible = false;
            AnswerEntry4Border.IsVisible = false;
            CorrectAnswer4.IsVisible = false;
            answerImage4Frame.IsVisible = false;

            AnswerEntry2.IsVisible = false;
            AnswerEntry2Border.IsVisible = false;
            CorrectAnswer2.IsVisible = false;
            answerImage2Frame.IsVisible = false;

            AnswerEntry3.IsVisible = false;
            AnswerEntry3Border.IsVisible = false;
            CorrectAnswer3.IsVisible = false;
            answerImage3Frame.IsVisible = false;

            await answersScrollView.ScrollToAsync(AnswerEntry1, ScrollToPosition.Start, true);
        }
        else if (sender == AnswerEntry2)
        {
            AnswerEntry2Border.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            AnswerEntry2Border.StrokeThickness = 4;
            await AnswerEntry2Border.ScaleTo(1.05, 120, Easing.CubicOut);

            AnswerEntry1.IsVisible = false;
            AnswerEntry1Border.IsVisible = false;
            CorrectAnswer1.IsVisible = false;
            answerImage1Frame.IsVisible = false;

            AnswerEntry4.IsVisible = false;
            AnswerEntry4Border.IsVisible = false;
            CorrectAnswer4.IsVisible = false;
            answerImage4Frame.IsVisible = false;

            AnswerEntry3.IsVisible = false;
            AnswerEntry3Border.IsVisible = false;
            CorrectAnswer3.IsVisible = false;
            answerImage3Frame.IsVisible = false;

            await answersScrollView.ScrollToAsync(AnswerEntry2, ScrollToPosition.Start, true);
        }
        else if (sender == AnswerEntry3)
        {
            AnswerEntry3Border.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            AnswerEntry3Border.StrokeThickness = 4;
            await AnswerEntry3Border.ScaleTo(1.05, 120, Easing.CubicOut);

            AnswerEntry1.IsVisible = false;
            AnswerEntry1Border.IsVisible = false;
            CorrectAnswer1.IsVisible = false;
            answerImage1Frame.IsVisible = false;

            AnswerEntry2.IsVisible = false;
            AnswerEntry2Border.IsVisible = false;
            CorrectAnswer2.IsVisible = false;
            answerImage2Frame.IsVisible = false;

            AnswerEntry4.IsVisible = false;
            AnswerEntry4Border.IsVisible = false;
            CorrectAnswer4.IsVisible = false;
            answerImage4Frame.IsVisible = false;

            await answersScrollView.ScrollToAsync(AnswerEntry3, ScrollToPosition.Start, true);
        }
        else if (sender == AnswerEntry4)
        {
            AnswerEntry4Border.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
            AnswerEntry4Border.StrokeThickness = 4;
            await AnswerEntry4Border.ScaleTo(1.05, 120, Easing.CubicOut);

            AnswerEntry1.IsVisible = false;
            AnswerEntry1Border.IsVisible = false;
            CorrectAnswer1.IsVisible = false;
            answerImage1Frame.IsVisible = false;

            AnswerEntry2.IsVisible = false;
            AnswerEntry2Border.IsVisible = false;
            CorrectAnswer2.IsVisible = false;
            answerImage2Frame.IsVisible = false;

            AnswerEntry3.IsVisible = false;
            AnswerEntry3Border.IsVisible = false;
            CorrectAnswer3.IsVisible = false;
            answerImage3Frame.IsVisible = false;

            await answersScrollView.ScrollToAsync(AnswerEntry4, ScrollToPosition.Start, true);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        QuestionEntry.Unfocus();
        AnswerEntry1.Unfocus();
        AnswerEntry2.Unfocus();
        AnswerEntry3.Unfocus();
        AnswerEntry4.Unfocus();
        return true;
    }

    private async void OnEntryUnfocused(object sender, FocusEventArgs e)
    {
        if (sender == QuestionEntry)
        {
            QuestionEntryBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            QuestionEntryBorder.StrokeThickness = 2;
            await QuestionEntryBorder.ScaleTo(1.0, 120, Easing.CubicOut);
        }
        else if (sender == AnswerEntry1)
        {
            AnswerEntry1Border.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            AnswerEntry1Border.StrokeThickness = 2;
            await AnswerEntry1Border.ScaleTo(1.0, 120, Easing.CubicOut);

            AnswerEntry4.IsVisible = true;
            AnswerEntry4Border.IsVisible = true;
            CorrectAnswer4.IsVisible = true;
            answerImage4Frame.IsVisible = true;

            AnswerEntry2.IsVisible = true;
            AnswerEntry2Border.IsVisible = true;
            CorrectAnswer2.IsVisible = true;
            answerImage2Frame.IsVisible = true;

            AnswerEntry3.IsVisible = true;
            AnswerEntry3Border.IsVisible = true;
            CorrectAnswer3.IsVisible = true;
            answerImage3Frame.IsVisible = true;

            await answersScrollView.ScrollToAsync(AnswerEntry1, ScrollToPosition.Start, true);
        }
        else if (sender == AnswerEntry2)
        {
            AnswerEntry2Border.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            AnswerEntry2Border.StrokeThickness = 2;
            await AnswerEntry2Border.ScaleTo(1.0, 120, Easing.CubicOut);

            AnswerEntry1.IsVisible = true;
            AnswerEntry1Border.IsVisible = true;
            CorrectAnswer1.IsVisible = true;
            answerImage1Frame.IsVisible = true;

            AnswerEntry4.IsVisible = true;
            AnswerEntry4Border.IsVisible = true;
            CorrectAnswer4.IsVisible = true;
            answerImage4Frame.IsVisible = true;

            AnswerEntry3.IsVisible = true;
            AnswerEntry3Border.IsVisible = true;
            CorrectAnswer3.IsVisible = true;
            answerImage3Frame.IsVisible = true;

            await answersScrollView.ScrollToAsync(AnswerEntry1, ScrollToPosition.Start, true);
        }
        else if (sender == AnswerEntry3)
        {
            AnswerEntry3Border.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            AnswerEntry3Border.StrokeThickness = 2;
            await AnswerEntry3Border.ScaleTo(1.0, 120, Easing.CubicOut);

            AnswerEntry1.IsVisible = true;
            AnswerEntry1Border.IsVisible = true;
            CorrectAnswer1.IsVisible = true;
            answerImage1Frame.IsVisible = true;

            AnswerEntry2.IsVisible = true;
            AnswerEntry2Border.IsVisible = true;
            CorrectAnswer2.IsVisible = true;
            answerImage2Frame.IsVisible = true;

            AnswerEntry4.IsVisible = true;
            AnswerEntry4Border.IsVisible = true;
            CorrectAnswer4.IsVisible = true;
            answerImage4Frame.IsVisible = true;

            await answersScrollView.ScrollToAsync(AnswerEntry1, ScrollToPosition.Start, true);
        }
        else if (sender == AnswerEntry4)
        {
            AnswerEntry4Border.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
            AnswerEntry4Border.StrokeThickness = 2;
            await AnswerEntry4Border.ScaleTo(1.0, 120, Easing.CubicOut);

            AnswerEntry1.IsVisible = true;
            AnswerEntry1Border.IsVisible = true;
            CorrectAnswer1.IsVisible = true;
            answerImage1Frame.IsVisible = true;

            AnswerEntry2.IsVisible = true;
            AnswerEntry2Border.IsVisible = true;
            CorrectAnswer2.IsVisible = true;
            answerImage2Frame.IsVisible = true;

            AnswerEntry3.IsVisible = true;
            AnswerEntry3Border.IsVisible = true;
            CorrectAnswer3.IsVisible = true;
            answerImage3Frame.IsVisible = true;

            await answersScrollView.ScrollToAsync(AnswerEntry1, ScrollToPosition.Start, true);
        }
    }

    private void OnEntryCompleted(object sender, EventArgs e)
    {
        if (sender is Entry entry)
            entry.Unfocus();
    }
}