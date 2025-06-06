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

    private async void onAnswer1_Clicked(object sender, EventArgs e)
    {
        var frame = (Frame)sender;

        frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        frame.Background = (Color)Application.Current.Resources["Cerulean"];

        await frame.ScaleTo(0.9, 180, Easing.CubicIn);
        await frame.ScaleTo(1.0, 180, Easing.CubicOut);

        frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        frame.Background = (Color)Application.Current.Resources["PictonBlue"];

        var question = randomizedQuestions[currQuestion];
        var selected = question.Answers[0];

        QuizStartPage.AnsweredQuestions.Add(new AnsweredQuestion
        {
            Question = question,
            SelectedAnswer = selected
        });

        if (selected.IsCorrect)
            correctAnswers++;

        currQuestion++;
        NextQuestion();
    }

    private async void onAnswer2_Clicked(object sender, EventArgs e)
    {
        var frame = (Frame)sender;

        frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        frame.Background = (Color)Application.Current.Resources["Cerulean"];

        await frame.ScaleTo(0.9, 180, Easing.CubicIn);
        await frame.ScaleTo(1.0, 180, Easing.CubicOut);

        frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        frame.Background = (Color)Application.Current.Resources["PictonBlue"];

        var question = randomizedQuestions[currQuestion];
        var selected = question.Answers[1];

        QuizStartPage.AnsweredQuestions.Add(new AnsweredQuestion
        {
            Question = question,
            SelectedAnswer = selected
        });

        if (selected.IsCorrect)
            correctAnswers++;

        currQuestion++;
        NextQuestion();
    }

    private async void onAnswer3_Clicked(object sender, EventArgs e)
    {
        var frame = (Frame)sender;

        frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        frame.Background = (Color)Application.Current.Resources["Cerulean"];

        await frame.ScaleTo(0.9, 180, Easing.CubicIn);
        await frame.ScaleTo(1.0, 180, Easing.CubicOut);

        frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        frame.Background = (Color)Application.Current.Resources["PictonBlue"];

        var question = randomizedQuestions[currQuestion];
        var selected = question.Answers[2];

        QuizStartPage.AnsweredQuestions.Add(new AnsweredQuestion
        {
            Question = question,
            SelectedAnswer = selected
        });

        if (selected.IsCorrect)
            correctAnswers++;

        currQuestion++;
        NextQuestion();
    }

    private async void onAnswer4_Clicked(object sender, EventArgs e)
    {
        var frame = (Frame)sender;

        frame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        frame.Background = (Color)Application.Current.Resources["Cerulean"];

        await frame.ScaleTo(0.9, 180, Easing.CubicIn);
        await frame.ScaleTo(1.0, 180, Easing.CubicOut);

        frame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        frame.Background = (Color)Application.Current.Resources["PictonBlue"];

        var question = randomizedQuestions[currQuestion];
        var selected = question.Answers[3];

        QuizStartPage.AnsweredQuestions.Add(new AnsweredQuestion
        {
            Question = question,
            SelectedAnswer = selected
        });

        if (selected.IsCorrect)
            correctAnswers++;

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

        var question = randomizedQuestions[currQuestion];
        QuestionLabel.Text = randomizedQuestions[currQuestion].QuestionText;
        ShowAnswersGrid();

        // Wyświetl obraz pytania jeśli jest
        if (!string.IsNullOrEmpty(question.QuestionImage))
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(question.QuestionImage);
                QuestionImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                QuestionImage.IsVisible = true;
                imageBorder.IsVisible = true;
                QuestionImagePlaceholder.IsVisible = false;
            }
            catch
            {
                QuestionImage.IsVisible = false;
                imageBorder.IsVisible = false;
                QuestionImagePlaceholder.IsVisible = true;
            }
        }
        else
        {
            QuestionImage.IsVisible = false;
            imageBorder.IsVisible = false;
            QuestionImagePlaceholder.IsVisible = true;
        }
    }

    private async void NextQuestion()
    {
        if (currQuestion < QNo)
        {
            var question = randomizedQuestions[currQuestion];
            QuestionLabel.Text = randomizedQuestions[currQuestion].QuestionText;
            ShowAnswersGrid();

            // Wyświetl obraz pytania jeśli jest
            if (!string.IsNullOrEmpty(question.QuestionImage))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(question.QuestionImage);
                    QuestionImage.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    QuestionImage.IsVisible = true;
                    imageBorder.IsVisible = true;
                    QuestionImagePlaceholder.IsVisible = false;
                }
                catch
                {
                    QuestionImage.IsVisible = false;
                    imageBorder.IsVisible = false;
                    QuestionImagePlaceholder.IsVisible = true;
                }
            }
            else
            {
                QuestionImage.IsVisible = false;
                imageBorder.IsVisible = false;
                QuestionImagePlaceholder.IsVisible = true;
            }
        }
        else
        {
            await Shell.Current.GoToAsync($"///ResultPage?param={correctAnswers}&paramPath={Path}&QuestionParam={QNo}");
        }
    }

    private void ShowAnswersGrid()
    {
        AnswersGrid.Children.Clear();
        AnswersGrid.RowDefinitions.Clear();
        AnswersGrid.ColumnDefinitions.Clear();

        var answers = randomizedQuestions[currQuestion].Answers;
        int count = answers.Count;

        AnswersGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        AnswersGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

        int rows = (count + 1) / 2;
        for (int i = 0; i < rows; i++)
            AnswersGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

        for (int i = 0; i < count; i++)
        {
            var answer = answers[i];

            var stack = new VerticalStackLayout
            {
                Spacing = 8,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            if (!string.IsNullOrEmpty(answer.AnswerImage))
            {
                var height = 0;

                if (string.IsNullOrEmpty(answer.AnswerText))
                {
                    height = 150;
                }
                else
                {
                    height = 80;
                }
                    try
                    {
                        var img = new Image
                        {
                            Source = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(answer.AnswerImage))),
                            MaximumHeightRequest = height,
                            MaximumWidthRequest = 150,
                            Aspect = Aspect.AspectFit,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center
                        };
                        stack.Children.Add(img);
                    }
                    catch { /* ignoruj błędy dekodowania */ }
            }

            if (!string.IsNullOrEmpty(answer.AnswerText)) {
                var fontSize = 16;

                if (!string.IsNullOrEmpty(answer.AnswerImage)) {
                    fontSize = 10;
                }

                stack.Children.Add(new Label
                {
                    Text = answer.AnswerText,
                    FontSize = fontSize,
                    HorizontalTextAlignment = TextAlignment.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontFamily = "Ubuntu-Regular",
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Colors.White
                });
            }

            var frame = new Frame
            {
                Content = stack,
                BackgroundColor = (Color)Application.Current.Resources["PictonBlue"],
                BorderColor = Colors.Transparent,
                CornerRadius = 12,
                HeightRequest = 150,
                WidthRequest = 150,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Padding = 8
            };

            var tapGesture = new TapGestureRecognizer();

            switch (i)
            {
                case 0: tapGesture.Tapped += onAnswer1_Clicked; break;
                case 1: tapGesture.Tapped += onAnswer2_Clicked; break;
                case 2: tapGesture.Tapped += onAnswer3_Clicked; break;
                case 3: tapGesture.Tapped += onAnswer4_Clicked; break;
            }

            frame.GestureRecognizers.Add(tapGesture);

            int row = i / 2;
            int col = i % 2;

            Grid.SetRow(frame, row);
            Grid.SetColumn(frame, col);

            AnswersGrid.Children.Add(frame);
        }
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }
}