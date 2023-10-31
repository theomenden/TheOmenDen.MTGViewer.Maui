using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.Logging;
using MTGViewer.Maui.Data;
using MTGViewer.Maui.Extensions;
using Serilog;
using Serilog.Events;

namespace MTGViewer.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMemoryUsage()
            .WriteTo.Async(a =>
            {
                a.File("./logs/log-.txt", rollingInterval: RollingInterval.Day);
                a.Console();
                a.BrowserConsole();
            })
            .CreateBootstrapLogger();
        try
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular"); });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddLogging(options => options.AddSerilog(dispose: true));
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddBlazorise()
                .AddBootstrap5Providers()
                .AddBootstrap5Components()
                .AddFontAwesomeIcons();
            builder.Services.AddScryfallApiServices();
            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
