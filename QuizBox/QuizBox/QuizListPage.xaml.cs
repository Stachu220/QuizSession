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
                CommandParameter = filePath,
                BackgroundColor = Color.FromHex("#FFCC00"),
                TextColor = Color.FromHex("#000000"),
                Margin = new Thickness(5),
                Padding = new Thickness(10)
            };
            quizButton.Clicked += async (s, e) =>
            {
                // Navigate to the quiz page with the selected quiz file path
                await Shell.Current.GoToAsync($"///QuizStartPage?param={filePath}");
            };
            QuizListStack.Children.Add(quizButton);
        }
    }

    private void onGoBack(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"///MainPage");
    }
}