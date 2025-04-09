namespace QuizBox
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

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
    }

}
