using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Soenneker.Bland.Client.Abstract;

/// <summary>
/// Provides the cached, authenticated <see cref="HttpClient"/> used for Bland.ai API requests.
/// </summary>
public interface IBlandClientUtil : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Gets the named HTTP client, creating and configuring it on first use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel client creation.</param>
    /// <returns>The cached HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
