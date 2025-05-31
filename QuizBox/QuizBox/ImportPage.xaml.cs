using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
        string path = FileSystem.AppDataDirectory;
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
                    await DisplayAlert("Error", "Invalid quiz file format.", "OK");
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
            await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
        }

    }
}