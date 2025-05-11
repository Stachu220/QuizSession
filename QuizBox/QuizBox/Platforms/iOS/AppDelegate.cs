using Foundation;
using UIKit;
using static Microsoft.Maui.LifecycleEvents.iOSLifecycle;

namespace QuizBox
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
