namespace QuizBox;

public partial class QuizListPage : ContentPage
{
	public QuizListPage()
	{
		InitializeComponent();
	}

    private void ScrollView_Loaded(object sender, EventArgs e)
    {
        string path = FileSystem.AppDataDirectory;
        string[] files = Directory.GetFiles(path, "*.json");

        if (QuizListStack.Children.Count != 0)
        {
            QuizListStack.Children.Clear();
        }

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file);
            string filePath = Path.Combine(path, file);
            //add child to QuizListStack
            var quizButton = new Button
            {
                Text = fileName,
                CornerRadius = 12,
                CommandParameter = filePath,
                BackgroundColor = (Color)Application.Current.Resources["PictonBlue"],
                FontFamily = "Ubuntu-Regular",
                FontSize=16,
                FontAttributes=FontAttributes.Bold,
                TextColor = Color.FromHex("#FFFFFF"),
                Margin = new Thickness(10),
                Padding = new Thickness(15)
            };
            quizButton.Clicked += async (s, e) =>
            {
                quizButton.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
                quizButton.Background = (Color)Application.Current.Resources["Cerulean"];

                await quizButton.ScaleTo(0.9, 180, Easing.CubicIn);
                await quizButton.ScaleTo(1.0, 180, Easing.CubicOut);

                quizButton.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
                quizButton.Background = (Color)Application.Current.Resources["PictonBlue"];

                await Shell.Current.GoToAsync($"///QuizStartPage?param={filePath}");
            };
            QuizListStack.Children.Add(quizButton);
        }
    }

    private async void onGoBack(object sender, EventArgs e)
    {
        backBtn.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        backBtn.Background = (Color)Application.Current.Resources["Cerulean"];

        await backBtn.ScaleTo(0.85, 180, Easing.CubicIn);
        await backBtn.ScaleTo(1.0, 180, Easing.CubicOut);

        backBtn.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        backBtn.Background = (Color)Application.Current.Resources["PictonBlue"];

        await Shell.Current.GoToAsync($"///MainPage");
    }
}