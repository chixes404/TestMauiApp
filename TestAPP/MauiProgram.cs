using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using TestAPP.Popup;
using TestAPP.Services;
using TestAPP.ViewModels;

namespace TestAPP
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FredokaOne-Regular.ttf", "FredokaOne");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
                });



            builder.Services.AddSingleton<ApiService>();
            builder.Services.AddSingleton<UserGuildsViewModel>();
            builder.Services.AddSingleton<GuildsListViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<GuildsPage>();
            builder.Services.AddSingleton<LocationPermissionPopup>();





#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
