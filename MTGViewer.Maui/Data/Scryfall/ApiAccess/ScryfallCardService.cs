using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MTGViewer.Maui.Data.Scryfall.Models;
using System.Net;
using System.Text.Json;
using JetBrains.Annotations;
using TheOmenDen.Shared.Extensions;
using TheOmenDen.Shared.Responses;

namespace MTGViewer.Maui.Data.Scryfall.ApiAccess;
public sealed class ScryfallCardService : ApiServiceBase<ScryfallCard>
{
    private readonly ILogger _logger;
    private const string CardsEndPoint = "cards/";

    public ScryfallCardService(IHttpClientFactory httpClientFactory,
        IOptions<HttpClientConfiguration> httpClientOptions,
        ILogger<ScryfallCardService> logger)
    : base(httpClientFactory, httpClientOptions)
    {
        _logger = logger;
    }

    public override async Task<ApiResponse<ScryfallCard>> GetContentAsync([CanBeNull] string uri, CancellationToken cancellationToken = default)
    {
        var apiResponse = new ApiResponse<ScryfallCard>();

        if (string.IsNullOrWhiteSpace(uri))
        {
            uri = "random";
        }

        try
        {
            apiResponse = await base.GetContentAsync($"{CardsEndPoint}{uri}", cancellationToken);
            apiResponse.Outcome = OperationOutcome.SuccessfulOutcome;
        }
        catch (HttpRequestException ex)
        {
            apiResponse.Outcome = OperationOutcome.UnsuccessfulOutcome;

            apiResponse.Outcome.CorrelationId = Guid.NewGuid().ToString();

            apiResponse.Outcome.ClientErrorPayload.Message = ex.Message;

            _logger.LogError("Failed retrieving symbols from scryfall, Exception was: {@ex}", ex);

        }
        catch (HttpListenerException ex)
        {
            apiResponse.Outcome = OperationOutcome.UnsuccessfulOutcome;

            apiResponse.Outcome.CorrelationId = Guid.NewGuid().ToString();

            apiResponse.Outcome.ClientErrorPayload.Message = ex.Message;

            _logger.LogError("Failed retrieving symbols from scryfall, Exception was: {@ex}", ex);
        }
        catch (JsonException ex)
        {
            apiResponse.Outcome = OperationOutcome.UnsuccessfulOutcome;

            apiResponse.Outcome.CorrelationId = Guid.NewGuid().ToString();

            apiResponse.Outcome.ClientErrorPayload.Message = ex.Message;

            _logger.LogError("Failed retrieving symbols from scryfall, Exception was: {@ex}", ex);
        }
        catch (NullReferenceException ex)
        {
            var errors = new List<String>(2);

            var innermostExceptionMessage = ex.GetInnermostExceptionMessage();

            errors.Add(ex.Message);
            errors.Add(innermostExceptionMessage);

            apiResponse.Outcome = OperationOutcome.ValidationFailureOutcome(errors, "Could not get symbols deserialized");
        }
        catch (Exception ex)
        {
            apiResponse.Outcome = OperationOutcome.UnsuccessfulOutcome;

            apiResponse.Outcome.CorrelationId = Guid.NewGuid().ToString();

            apiResponse.Outcome.ClientErrorPayload.Message = ex.Message;

            _logger.LogError("Failed retrieving symbols from scryfall, Exception was: {@ex}", ex);
        }

        return apiResponse;
    }
}
