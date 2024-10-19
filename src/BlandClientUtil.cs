using Soenneker.Bland.Client.Abstract;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using Soenneker.Utils.HttpClientCache.Abstract;
using Soenneker.Utils.HttpClientCache.Dtos;

namespace Soenneker.Bland.Client;

/// <inheritdoc cref="IBlandClient"/>
public class BlandClient: IBlandClient
{
    private readonly IHttpClientCache _httpClientCache;

    private readonly HttpClientOptions _options = new() { BaseAddress = "https://api.bland.ai/v1/" };

    public BlandClient(IHttpClientCache httpClientCache)
    {
        _httpClientCache = httpClientCache;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(BlandClient), _options, cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClientCache.RemoveSync(nameof(BlandClient));
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _httpClientCache.Remove(nameof(BlandClient));
    }
}
