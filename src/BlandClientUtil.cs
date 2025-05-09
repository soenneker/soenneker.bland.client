using Microsoft.Extensions.Configuration;
using Soenneker.Bland.Client.Abstract;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.String;
using Soenneker.Utils.HttpClientCache.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Bland.Client;

/// <inheritdoc cref="IBlandClientUtil"/>
public class BlandClientUtil : IBlandClientUtil
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _configuration;
    private const string _clientId = nameof(BlandClientUtil);

    public BlandClientUtil(IHttpClientCache httpClientCache, IConfiguration configuration)
    {
        _httpClientCache = httpClientCache;
        _configuration = configuration;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_clientId, () =>
        {
            var apiKey = _configuration.GetValueStrict<string>("Bland:ApiKey");
            var encryptedKey = _configuration.GetValue<string>("Bland:EncryptedKey");

            var headers = new Dictionary<string, string>
            {
                { "authorization", apiKey }
            };

            if (encryptedKey.HasContent())
                headers.Add("encrypted_key", encryptedKey);

            return new HttpClientOptions
            {
                BaseAddress = "https://api.bland.ai/v1/",
                DefaultRequestHeaders = headers
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _httpClientCache.RemoveSync(_clientId);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _httpClientCache.Remove(_clientId);
    }
}
