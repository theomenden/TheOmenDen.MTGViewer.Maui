using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MTGViewer.Maui.Data.Scryfall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheOmenDen.Shared.Extensions;
using TheOmenDen.Shared.Responses;

namespace MTGViewer.Maui.Data.Scryfall.ApiAccess;
public sealed class ScryfallSymbologyService : ApiServiceBase<Symbology>
{
    private readonly ILogger<ScryfallSymbologyService> _logger;

    private const string SymbologyEndpoint = @"symbology/";

    public ScryfallSymbologyService(IHttpClientFactory httpClientFactory, IOptions<HttpClientConfiguration> options, ILogger<ScryfallSymbologyService> logger)
    : base(httpClientFactory, options)
    {
        _logger = logger;
    }

    public async Task<ApiResponse<IEnumerable<SymbologyDatum>>> GetAllSymbolsFromScryfall(CancellationToken cancellationToken = default)
    {
        var apiResponse = new ApiResponse<IEnumerable<SymbologyDatum>>();

        try
        {
            var content = await GetContentAsync(SymbologyEndpoint, cancellationToken);
            apiResponse.Outcome = OperationOutcome.SuccessfulOutcome;
            apiResponse.Data = content.Data.Data;
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
