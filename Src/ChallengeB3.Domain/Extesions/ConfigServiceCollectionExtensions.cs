using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ChallengeB3.Domain.Models;

namespace ChallengeB3.Domain.Extesions;

public static class ConfigServiceCollectionExtensions
{
    public static IServiceCollection AddAppConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<QueueSettings>(config.GetSection(nameof(QueueSettings)));

        return services;
    }
}
