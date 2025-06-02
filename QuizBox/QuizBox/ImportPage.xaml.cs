using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microsoft.Maui.Graphics.Text;
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
        backBtn.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
        backBtn.Background = (Color)Application.Current.Resources["Cerulean"];

        await backBtn.ScaleTo(0.85, 180, Easing.CubicIn);
        await backBtn.ScaleTo(1.0, 180, Easing.CubicOut);

        backBtn.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
        backBtn.Background = (Color)Application.Current.Resources["PictonBlue"];
        await Shell.Current.GoToAsync("///MainPage");
    }

	public async void onImportQuiz(object sender, EventArgs e)
	{
        //import quiz from file -> in reality copy it to working folder
        string path = FileSystem.AppDataDirectory;

        importQuizBtnBorder.Stroke = (Brush?)Application.Current.Resources["CeruleanBrush"];
        iconLabel.TextColor = (Color)Application.Current.Resources["Cerulean"];
        text1.TextColor = (Color)Application.Current.Resources["Cerulean"];
        text2.TextColor = (Color)Application.Current.Resources["Cerulean"];
        text3.TextColor = (Color)Application.Current.Resources["Cerulean"];
        text3.TextColor = (Color)Application.Current.Resources["Cerulean"];

        await importQuizBtnBorder.ScaleTo(0.9, 180, Easing.CubicIn);
        await importQuizBtnBorder.ScaleTo(1.0, 180, Easing.CubicOut);

        importQuizBtnBorder.Stroke = (Brush?)Application.Current.Resources["PictonBlueBrush"];
        iconLabel.TextColor = (Color)Application.Current.Resources["PictonBlue"];
        text1.TextColor = (Color)Application.Current.Resources["PictonBlue"];
        text2.TextColor = (Color)Application.Current.Resources["PictonBlue"];
        text3.TextColor = (Color)Application.Current.Resources["PictonBlue"];
        text3.TextColor = (Color)Application.Current.Resources["PictonBlue"];

        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Please select a quiz file",
                FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.Android, new[] { "application/json" } },
                    { DevicePlatform.iOS, new[] { "public.json" } },
                    { DevicePlatform.WinUI, new[] { ".json" } }
                })
            });

            if (result != null)
            {
                //read file content
                using var stream = await result.OpenReadAsync();

                //validate file content
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();

                Root? imported = JsonSerializer.Deserialize<Root>(json);

                if (imported.Quiz.FirstOrDefault() == null || imported.Quiz.Count == 0 || imported.Quiz[0].Name == null || imported.Quiz[0].Description == null ||
                    imported.Quiz[0].Questions == null || imported.Quiz[0].Questions.Count == 0 || imported.Quiz[0].Questions[0].QuestionText == null || imported.Quiz[0].Questions[0].Answers == null)
                {
                    await DisplayAlert("ERROR", "Invalid quiz file format.", "OK");
                    return;
                }

                //Define destination path
                string fileName = Path.GetFileName(result.FullPath);
                string destinationPath = Path.Combine(path, fileName);

                //using var OutputStream = File.Create(destinationPath);
                //await stream.CopyToAsync(OutputStream);
                File.WriteAllText(destinationPath, json);

                await Toast.Make($"File {fileName} imported successfully!", ToastDuration.Short).Show();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("ERROR", $"An error occurred: {ex.Message}", "OK");
        }

    }
}