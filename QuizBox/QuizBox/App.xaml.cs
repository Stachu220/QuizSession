using Plugin.Maui.Audio;
using QuizBox.Helpers;

namespace QuizBox
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnSleep()
        {
            MusicService.Instance.StopMusic();
        }

        protected override void OnResume()
        {
            MusicService.Instance.StartMusic();
        }
    }
}
