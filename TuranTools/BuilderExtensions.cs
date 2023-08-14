using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TuranTools;

public static class BuilderExtensions
{
    public static IServiceCollection AddCustomOptions<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string configKey,
        out T options
    ) where T : class, new()
    {
        services.Configure<T>(configuration.GetSection(configKey));
        options = new T();
        configuration.Bind(configKey, options);

        return services;
    }
    
    public static WebApplicationBuilder AddCustomOptions<T>(
        this WebApplicationBuilder builder,
        string configKey,
        out T options
    ) where T : class, new()
    {
        builder.Services.Configure<T>(builder.Configuration.GetSection(configKey));
        options = new T();
        builder.Configuration.Bind(configKey, options);

        return builder;
    }
}