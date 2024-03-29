using Microsoft.AspNetCore.Builder;

namespace TurTools.SecretAbstractions;

/// <summary>
/// Extensions on WebApplicationBuilder
/// </summary>
public static class BuilderExtensions
{
    /// <summary>
    /// Registers a configuration instance which an implementation of <see cref="OptionsWithSecrets"/> will bind against
    /// Before binding, properties of <see cref="OptionsWithSecrets"/> with the <see cref="SecretKeyAttribute"/> are
    /// retrieved by by the secretRetrievalDelegate
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> to add the options to</param>
    /// <param name="configKey">The key of the configuration section to register</param>
    /// <param name="secretRetrievalDelegate">A delegate which returns the secret corresponding to a given key (For example from Azure KeyVault)</param>
    /// <typeparam name="TOptionsWithSecrets">The options type to be configured</typeparam>
    /// <returns></returns>
    public static WebApplicationBuilder AddOptionsWithSecrets<TOptionsWithSecrets>(
        this WebApplicationBuilder builder,
        string configKey,
        Func<string, Task<string?>> secretRetrievalDelegate
    ) where TOptionsWithSecrets : OptionsWithSecrets, new()
    {
        builder.Services.AddOptionsWithSecrets<TOptionsWithSecrets>(
            configuration: builder.Configuration,
            configKey: configKey, 
            secretRetrievalDelegate: async secretKey => await secretRetrievalDelegate(secretKey)
        );

        return builder;
    }
}