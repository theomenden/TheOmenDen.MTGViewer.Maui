using MTGViewer.Maui.Data;
using Polly.Extensions.Http;
using Polly;
using MTGViewer.Maui.Data.Scryfall.ApiAccess;
using TheOmenDen.Shared.Interfaces.Services;

namespace MTGViewer.Maui.Extensions;
public static class ServiceCollectionExtensions
{
    private const string ScryfallApiEndpoint = "https://api.scryfall.com";

    public static IServiceCollection AddScryfallApiServices(this IServiceCollection services)
    {
        services.AddScoped<ScryfallCardService>();
        services.AddScoped<ScryfallSymbologyService>();
        services.AddScoped<ScryfallSetInformationService>();

        AddTheOmenDenHttpServices<ScryfallCardService>(services,
            new HttpClientConfiguration
            {
                BaseAddress = ScryfallApiEndpoint,
                Name = "Scryfall"
            });

        return services;
    }


    /// <summary>
    /// Adds Basic Api interaction via <see cref="ApiServiceBase{T}"/>
    /// </summary>
    /// <typeparam name="T">The containing DI type</typeparam>
    /// <param name="services">The existing service collection</param>
    /// <param name="httpClientConfiguration">Configuration for the named client</param>
    /// <returns>The same <see cref="IServiceCollection"/> for further chaining</returns>
    public static IServiceCollection AddTheOmenDenHttpServices<T>(this IServiceCollection services, HttpClientConfiguration httpClientConfiguration)
    {
        var containerType = typeof(T);

        services.AddOptions<HttpClientConfiguration>()
            .Configure(options =>
            {
                options.Name = httpClientConfiguration.Name;
                options.BaseAddress = httpClientConfiguration.BaseAddress;
            });

        services.AddHttpClient<IApiService>(httpClientConfiguration.Name, options =>
            {
                options.BaseAddress = new Uri(httpClientConfiguration.BaseAddress);
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IApiService), containerType)
            .AddClasses(c => c.AssignableTo(typeof(ApiServiceBase<>)))
            .AsImplementedInterfaces());

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}