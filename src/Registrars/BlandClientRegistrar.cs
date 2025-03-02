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
    /// Adds <see cref="IBlandClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddBlandClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IBlandClientUtil, BlandClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IBlandClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddBlandClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IBlandClientUtil, BlandClientUtil>();

        return services;
    }
}