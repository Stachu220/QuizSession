using QuizBox.Model;
using System.Text.Json;

namespace QuizBox;

public partial class ImportPage : ContentPage
{
	public ImportPage()
	{
		InitializeComponent();
	}

	public async void onGoBack(object sender, EventArgs e)
    {
        // Go back to the previous page
        await Shell.Current.GoToAsync("///MainPage");
    }

	public async void onImportQuiz(object sender, EventArgs e)
	{
        //import quiz from file -> in reality copy it to working folder
        //for now read first json file from working folder
        string path = FileSystem.AppDataDirectory;




        //string[] files = Directory.GetFiles(path, "*.json");
        ////deserialize first json file and display it as displayalert
        //var file = files[0];

        //string jsonString = File.ReadAllText(file);
        //Root root = JsonSerializer.Deserialize<Root>(jsonString);
        //await DisplayAlert("Quiz", $"Title: {root.Quiz[0].Name}\nDescription: {root.Quiz[0].Description}", "OK");
        //await DisplayAlert("Quiz", $"Questions: {root.Quiz[0].Questions.Count}", "OK");
        //for (int i = 0; i < root.Quiz[0].Questions.Count; i++)
        //{
        //    await DisplayAlert("Quiz", $"Question {i + 1}: {root.Quiz[0].Questions[i].QuestionText}\n anwers and bools \n {root.Quiz[0].Questions[i].Answers[0].AnswerText} - {root.Quiz[0].Questions[i].Answers[0].IsCorrect}\n {root.Quiz[0].Questions[i].Answers[1].AnswerText} - {root.Quiz[0].Questions[i].Answers[1].IsCorrect}\n {root.Quiz[0].Questions[i].Answers[2].AnswerText} - {root.Quiz[0].Questions[i].Answers[2].IsCorrect}\n {root.Quiz[0].Questions[i].Answers[3].AnswerText} - {root.Quiz[0].Questions[i].Answers[3].IsCorrect}", "OK");
        //}
    }
}