using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Soenneker.Bland.Client.Abstract;

/// <summary>
/// An async thread-safe singleton for Bland.ai's HTTP client
/// </summary>
public interface IBlandClient : IAsyncDisposable, IDisposable
{
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}