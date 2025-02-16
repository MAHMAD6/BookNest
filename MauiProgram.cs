using BookNest.Models;
using BookNest.ViewModels.Components;
using Microsoft.Extensions.Logging;

namespace BookNest
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Nexa-ExtraLight.ttf", "NexaExtraLight");
                    fonts.AddFont("Nexa-Heavy.ttf", "NexaHeavy");
                    fonts.AddFont("Aisling.otf", "Aisling");
                });
            SQLitePCL.Batteries_V2.Init();
            builder.Services.AddSingleton<BaseRepository<User>>();
            builder.Services.AddSingleton<BaseRepository<Book>>();
            builder.Services.AddSingleton<BaseRepository<Author>>();
            builder.Services.AddSingleton<BaseRepository<Category>>();
            builder.Services.AddSingleton<BaseRepository<Transaction>>();
            builder.Services.AddSingleton<BaseRepository<Fine>>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
