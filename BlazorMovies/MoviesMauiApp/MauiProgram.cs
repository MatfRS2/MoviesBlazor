using Microsoft.AspNetCore.Components.WebView.Maui;
using MoviesMauiApp.Data;
using MoviesMauiApp.Services;

namespace MoviesMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddScoped<IFilmoviService, FilmoviService>();
            builder.Services.AddScoped<IOsvezenjeService, OsvezenjeService>();

            return builder.Build();
        }
    }
}