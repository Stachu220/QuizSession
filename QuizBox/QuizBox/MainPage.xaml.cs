using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace QuizBox
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void goToQuizList (object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///QuizListPage");
        }

        private async void goToQuizCreator (object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///QuizCreatorPage");
        }

        private async void goToImport (object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ImportPage");
        }

        private async void exitBtn_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Exit", "Would you like to exit the app", "Yes", "No");
            if(answer)
            {
                Application.Current.Quit();
                await Toast.Make("Exiting the app!", ToastDuration.Short).Show();
            }
        }
    }

}
