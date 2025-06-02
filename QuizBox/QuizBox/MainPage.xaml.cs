using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Runtime.CompilerServices;

namespace QuizBox
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {

            base.OnAppearing();
            await Task.Delay(100);

            titleCard.Opacity = 0;
            titleCard.TranslationY = -titleCard.Height;

            mainIcon.Opacity = 0;
            mainIcon.Scale = 0.5;

            var titleCardAnim = Task.WhenAll(
                titleCard.TranslateTo(0, 0, 500, Easing.CubicOut),
                titleCard.FadeTo(1, 500, Easing.CubicIn)
            );

            var mainIconAnim = Task.WhenAll(
                mainIcon.FadeTo(1, 750, Easing.CubicIn),
                mainIcon.ScaleTo(1.0, 750, Easing.CubicOut)
            );

            await Task.WhenAll(titleCardAnim, mainIconAnim);
        }

        private async void goToQuizList (object sender, EventArgs e)
        {
            goToQuizListFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
            goToQuizListFrame.Background = (Color)Application.Current.Resources["Cerulean"];

            await goToQuizListFrame.ScaleTo(0.9, 180, Easing.CubicIn);
            await goToQuizListFrame.ScaleTo(1.0, 180, Easing.CubicOut);

            goToQuizListFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
            goToQuizListFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

            await Shell.Current.GoToAsync("///QuizListPage");
        }

        private async void goToQuizCreator (object sender, EventArgs e)
        {
            goToQuizCreatorFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
            goToQuizCreatorFrame.Background = (Color)Application.Current.Resources["Cerulean"];

            await goToQuizCreatorFrame.ScaleTo(0.9, 180, Easing.CubicIn);
            await goToQuizCreatorFrame.ScaleTo(1.0, 180, Easing.CubicOut);

            goToQuizCreatorFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
            goToQuizCreatorFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

            await Shell.Current.GoToAsync("///QuizCreatorPage");
        }

        private async void goToImport (object sender, EventArgs e)
        {
            goToImportFrame.BackgroundColor = (Color)Application.Current.Resources["Cerulean"];
            goToImportFrame.Background = (Color)Application.Current.Resources["Cerulean"];

            await goToImportFrame.ScaleTo(0.9, 180, Easing.CubicIn);
            await goToImportFrame.ScaleTo(1.0, 180, Easing.CubicOut);

            goToImportFrame.BackgroundColor = (Color)Application.Current.Resources["PictonBlue"];
            goToImportFrame.Background = (Color)Application.Current.Resources["PictonBlue"];

            await Shell.Current.GoToAsync("///ImportPage");
        }

        private async void exitBtn_Clicked(object sender, EventArgs e)
        {
            exitBtn.BackgroundColor = Color.FromHex("#B31900");
            exitBtn.Background = Color.FromHex("#B31900");

            await exitBtn.ScaleTo(0.85, 180, Easing.CubicIn);
            await exitBtn.ScaleTo(1.0, 180, Easing.CubicOut);

            exitBtn.BackgroundColor = Color.FromHex("#FF2400");
            exitBtn.Background = Color.FromHex("#FF2400");

            bool answer = await DisplayAlert("EXIT", "Would you like to exit the app?", "YES", "NO");
            if(answer)
            {
                Application.Current.Quit();
                await Toast.Make("Exiting the app!", ToastDuration.Short).Show();
            }
        }
    }

}
