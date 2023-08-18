using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TurTools.SecretAbstractions;

/// <summary>
/// Extensions on IServiceCollection
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers a configuration instance which an implementation of <see cref="OptionsWithSecrets"/> will bind against
    /// Before binding, properties of <see cref="OptionsWithSecrets"/> with the <see cref="SecretKeyAttribute"/> are
    /// retrieved by by the secretRetrievalDelegate
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The configuration containing the section to be registered</param>
    /// <param name="configKey">The key of the configuration section to register</param>
    /// <param name="secretRetrievalDelegate">A delegate which returns the secret corresponding to a given key (For example from Azure KeyVault)</param>
    /// <typeparam name="TOptionsWithSecrets">The options type to be configured</typeparam>
    /// <returns></returns>
    public static IServiceCollection AddOptionsWithSecrets<TOptionsWithSecrets>(
        this IServiceCollection services,
        IConfiguration configuration, 
        string configKey,
        Func<string, Task<string?>> secretRetrievalDelegate
    ) where TOptionsWithSecrets : OptionsWithSecrets, new()
    {
        services.Configure<TOptionsWithSecrets>(configuration.GetSection(configKey));
        services.Configure<TOptionsWithSecrets>(options =>
        {
            var task = options.PopulateSecrets(async secretKey => await secretRetrievalDelegate(secretKey));
            task.GetAwaiter().GetResult();
        });

        return services;
    }
}