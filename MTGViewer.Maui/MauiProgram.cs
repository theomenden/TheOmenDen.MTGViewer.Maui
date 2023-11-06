using System.Reflection;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Blazorise.LoadingIndicator;
using GraphQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MTGViewer.Maui.Extensions;
using Serilog;
using Serilog.Events;

namespace MTGViewer.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithThreadId()
            .Enrich.WithProcessName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMemoryUsage()
            .WriteTo.Debug()
            .WriteTo.BrowserConsole()
            .CreateBootstrapLogger();
        try
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            using var stream = currentAssembly.GetManifestResourceStream("MTGViewer.Maui.appsettings.json");

            var appSettingsJson = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                     .Build();

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular"); });

            builder.Configuration.AddConfiguration(appSettingsJson);

            builder.Services.AddGraphQL(options => options
                .AddSystemTextJson()
                .AddSchema<>()
                .UseMemoryCache());

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
            builder.Services.AddLoadingIndicator();
            builder.Services.AddScryfallApiServices();

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
