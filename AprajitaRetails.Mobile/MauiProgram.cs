using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace AprajitaRetails.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiCommunityToolkitCore().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
            }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
           // builder.Services.AddDevHttpClient(7030);
#endif
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("Server Address") });
            return builder.Build();
        }
    }
}