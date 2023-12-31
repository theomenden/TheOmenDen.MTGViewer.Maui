﻿using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using JetBrains.Annotations;
using TheOmenDen.Shared.Interfaces.Services;
using TheOmenDen.Shared.Responses;

namespace MTGViewer.Maui.Data;
public abstract class ApiServiceBase<TResponse> : IApiService<TResponse>
{
    protected readonly IHttpClientFactory ClientFactory;
    protected readonly HttpClientConfiguration HttpClientConfiguration;

    protected ApiServiceBase(IHttpClientFactory clientFactory, IOptions<HttpClientConfiguration> options)
    {
        ClientFactory = clientFactory;

        HttpClientConfiguration = options.Value;
    }

    /// <summary>
    /// Initiates a <see cref="HttpMethod.Get"/> request to retrieves content from a particular API endpoint given by the provided <paramref name="uri"/>
    /// </summary>
    /// <param name="uri">The endpoint we mean to retrieve from</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ApiResponse"/> of type <typeparamref name="TResponse"/></returns>
    public virtual async Task<ApiResponse<TResponse>> GetContentAsync([CanBeNull] String uri, CancellationToken cancellationToken = default)
    {
        using var client = ClientFactory.CreateClient(HttpClientConfiguration.Name);

        using var request = new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}{uri}");

        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var statusCode = (int)response.StatusCode;

        return new()
        {
            Data = await DeserializeFromStreamAsync<TResponse>(stream, cancellationToken),
            StatusCode = statusCode,
            Outcome = response.IsSuccessStatusCode ? OperationOutcome.SuccessfulOutcome : OperationOutcome.UnsuccessfulOutcome,
        };
    }

    /// <summary>
    /// Initiates a <see cref="HttpMethod.Get"/> request to retrieve content from a particular API endpoint given by the provided <paramref name="uri"/>
    /// </summary>
    /// <param name="uri">The endpoint we mean to retrieve from</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ApiResponse{T}"/> <see cref="IEnumerable{T}"/> type <typeparamref name="TResponse"/></returns>
    public virtual async Task<ApiResponse<IEnumerable<TResponse>>> GetContentStreamAsync(String uri, CancellationToken cancellationToken = default)
    {
        using var client = ClientFactory.CreateClient(HttpClientConfiguration.Name);

        using var request = new HttpRequestMessage(HttpMethod.Get, $"{client.BaseAddress}{uri}");

        using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        var statusCode = (int)response.StatusCode;

        return new()
        {
            Data = await DeserializeFromStreamAsync<IEnumerable<TResponse>>(stream, cancellationToken),
            StatusCode = statusCode,
            Outcome = response.IsSuccessStatusCode ? OperationOutcome.SuccessfulOutcome : OperationOutcome.UnsuccessfulOutcome,
        };
    }

    /// <summary>
    /// Initiates a <see cref="HttpMethod.Post"/> request to a particular endpoint provided by <paramref name="uri"/>, with the 
    /// </summary>
    /// <param name="uri">The endpoint we're aiming to hit</param>
    /// <param name="body">The content we want to post</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ApiResponse{T}"/> for processing further</returns>
    public virtual async Task<ApiResponse<String>> PostContentAsync(String uri, TResponse body, CancellationToken cancellationToken = default)
    {
        using var client = ClientFactory.CreateClient(HttpClientConfiguration.Name);

        var payload = JsonSerializer.Serialize(body);

        using var request = new HttpRequestMessage(HttpMethod.Post, $"{client.BaseAddress}{uri}");

        request.Content = new StringContent(payload, Encoding.UTF8, MediaTypeNames.Application.Json);

        using var httpResponse = await client.SendAsync(request, cancellationToken);

        var statusCode = (int)httpResponse.StatusCode;

        await using var content = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);

        return new()
        {
            Data = await DeserializeStreamToStringAsync(content),
            Outcome = httpResponse.IsSuccessStatusCode ? OperationOutcome.SuccessfulOutcome : OperationOutcome.UnsuccessfulOutcome,
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Initiates a <see cref="HttpMethod.Delete"/> to a particular endpoint provided by <paramref name="uri"/>, with the 
    /// </summary>
    /// <param name="uri">The endpoint we're aiming to hit</param>
    /// <param name="body">The content we want to post</param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="ApiResponse{T}"/> for processing further</returns>
    public virtual async Task<ApiResponse<String>> DeleteContentAsync(String uri,
        CancellationToken cancellationToken = default)
    {
        using var client = ClientFactory.CreateClient(HttpClientConfiguration.Name);

        using var request = new HttpRequestMessage(HttpMethod.Delete, $"{client.BaseAddress}{uri}");

        using var httpResponse = await client.SendAsync(request, cancellationToken);

        var statusCode = (int)httpResponse.StatusCode;

        await using var content = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);

        return new()
        {
            Data = await DeserializeStreamToStringAsync(content),
            Outcome = httpResponse.IsSuccessStatusCode ? OperationOutcome.SuccessfulOutcome : OperationOutcome.UnsuccessfulOutcome,
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Deserializes the provided <paramref name="stream"/> into <typeparamref name="TDeserialize"/>
    /// </summary>
    /// <typeparam name="TDeserialize">The entity we want to deserialize into</typeparam>
    /// <param name="stream">The given stream to deserialize using <see cref="JsonSerializer"/></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected virtual async Task<TDeserialize> DeserializeFromStreamAsync<TDeserialize>(Stream stream, CancellationToken cancellationToken)
    {
        if (stream is null || stream.CanRead is false)
        {
            return default;
        }

        var searchResult = await JsonSerializer.DeserializeAsync<TDeserialize>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);

        return searchResult;
    }

    /// <summary>
    /// Deserializes the provided <paramref name="stream"/> into a <see cref="String"/>
    /// </summary>
    /// <param name="stream">The provided stream we aim to deserialize using <see cref="JsonSer"/></param>
    /// <returns><see cref="String"/> for further processing</returns>
    protected virtual async Task<String> DeserializeStreamToStringAsync(Stream stream)
    {
        var content = String.Empty;

        if (stream is null)
        {
            return content;
        }

        using var sr = new StreamReader(stream);

        content = await sr.ReadToEndAsync();

        return content;
    }
}