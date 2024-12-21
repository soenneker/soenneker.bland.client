using Soenneker.Bland.Client.Abstract;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.Extensions.Configuration;
using Soenneker.Utils.HttpClientCache.Abstract;
using Soenneker.Utils.HttpClientCache.Dtos;
using Soenneker.Extensions.Configuration;

namespace Soenneker.Bland.Client;

/// <inheritdoc cref="IBlandClientUtil"/>
public class BlandClientUtil : IBlandClientUtil
{
    private readonly IHttpClientCache _httpClientCache;

    private readonly HttpClientOptions _options;

    public BlandClientUtil(IHttpClientCache httpClientCache, IConfiguration configuration)
    {
        _httpClientCache = httpClientCache;

        var apiKey = configuration.GetValueStrict<string>("Bland:ApiKey");
        var encryptedKey = configuration.GetValue<string>("Bland:EncryptedKey");

        _options = new HttpClientOptions
        {
            BaseAddress = "https://api.bland.ai/v1/",
            DefaultRequestHeaders = new System.Collections.Generic.Dictionary<string, string> {{"authorization", apiKey } }
        };

        if (encryptedKey is not null)
        {
            _options.DefaultRequestHeaders.Add("encrypted_key", encryptedKey);
        }
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(nameof(BlandClientUtil), _options, cancellationToken: cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClientCache.RemoveSync(nameof(BlandClientUtil));
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _httpClientCache.Remove(nameof(BlandClientUtil));
    }
}
