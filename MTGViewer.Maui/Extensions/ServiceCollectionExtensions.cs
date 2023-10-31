using MTGViewer.Maui.Data;
using Polly.Extensions.Http;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTGViewer.Maui.Data.Scryfall.ApiAccess;
using TheOmenDen.Shared.Interfaces.Services;

namespace MTGViewer.Maui.Extensions;
public static class ServiceCollectionExtensions
{
    private const string ScryfallApiEndpoint = "https://api.scryfall.com";

    public static IServiceCollection AddScryfallApiServices(this IServiceCollection services)
    {
        services.AddTransient<ScryfallCardService>();
        services.AddTransient<ScryfallSymbologyService>();
        services.AddTransient<ScryfallSetInformationService>();

        AddTheOmenDenHttpServices(services,
            new HttpClientConfiguration
            {
                BaseAddress = ScryfallApiEndpoint,
                Name = "Scryfall"
            });

        return services;
    }

    private static IServiceCollection AddTheOmenDenHttpServices(IServiceCollection services, HttpClientConfiguration httpClientConfiguration)
    {
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

        services.AddScoped(typeof(IApiService<>), typeof(ApiServiceBase<>));

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