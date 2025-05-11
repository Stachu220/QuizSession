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
            bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            if(answer)
            {
                Application.Current.Quit();
            }
        }
    }

}
