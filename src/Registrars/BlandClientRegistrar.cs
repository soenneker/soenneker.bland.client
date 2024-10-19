using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Bland.Client.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Bland.Client.Registrars;

/// <summary>
/// An async thread-safe singleton for Bland.ai's HTTP client
/// </summary>
public static class BlandClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IBlandClient"/> as a singleton service. <para/>
    /// </summary>
    public static void AddBlandClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCache();
        services.TryAddSingleton<IBlandClient, BlandClient>();
    }

    /// <summary>
    /// Adds <see cref="IBlandClient"/> as a scoped service. <para/>
    /// </summary>
    public static void AddBlandClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCache();
        services.TryAddScoped<IBlandClient, BlandClient>();
    }
}
