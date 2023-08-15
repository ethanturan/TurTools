using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TurTools.SecretAbstractions;

public static class BuilderExtensions
{
    public static async Task<T> AddOptionsWithSecrets<T>(
        this WebApplicationBuilder builder,
        string configKey,
        Func<string, Task<string?>> secretRetrievalDelegate
    ) where T : OptionsWithSecrets, new()
    {
        builder.Services.Configure<T>(builder.Configuration.GetSection(configKey));
        var options = new T();
        builder.Configuration.Bind(configKey, options);
        await options.PopulateSecrets(secretRetrievalDelegate);
        return options;
    }
}