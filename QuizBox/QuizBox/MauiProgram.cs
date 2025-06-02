using CommunityToolkit.Maui;
using FluentIcons.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;

namespace QuizBox
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "MaterialDesignIcons");
                    fonts.AddFont("Ubuntu-Regular.ttf", "Ubuntu-Regular");
                    fonts.AddFont("Ubuntu-Bold.ttf", "Ubuntu-Bold");
                    fonts.AddFont("Ubuntu-Italic.ttf", "Ubuntu-Italic");
                });

            

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
