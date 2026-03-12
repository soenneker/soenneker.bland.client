using System;
using Microsoft.Extensions.Configuration;
using Soenneker.Bland.Client.Abstract;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.String;
using Soenneker.Utils.HttpClientCache.Abstract;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Bland.Client;

/// <inheritdoc cref="IBlandClientUtil"/>
public sealed class BlandClientUtil : IBlandClientUtil
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly string _apiKey;
    private readonly string? _encryptedKey;
    private const string _clientId = nameof(BlandClientUtil);

    public BlandClientUtil(IHttpClientCache httpClientCache, IConfiguration configuration)
    {
        _httpClientCache = httpClientCache;
        _apiKey = configuration.GetValueStrict<string>("Bland:ApiKey");
        _encryptedKey = configuration.GetValue<string>("Bland:EncryptedKey");
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        // No closure: state passed explicitly + static lambda
        return _httpClientCache.Get(_clientId, (apiKey: _apiKey, encryptedKey: _encryptedKey), static state =>
        {
            var headers = new Dictionary<string, string>
            {
                { "authorization", state.apiKey }
            };

            if (state.encryptedKey.HasContent())
                headers.Add("encrypted_key", state.encryptedKey);

            return new HttpClientOptions
            {
                BaseAddress = new Uri("https://api.bland.ai/v1/"),
                DefaultRequestHeaders = headers
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_clientId);
    }
}