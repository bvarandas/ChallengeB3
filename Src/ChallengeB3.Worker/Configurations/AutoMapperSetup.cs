using AutoMapper;
using ChallengeB3.Application.AutoMapper;

namespace ChallengeB3.Queue.Worker.Configurations;

public static class AutoMapperSetup
{
    public static void AddAutoMapperSetup(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentException(nameof(services));

        var mapper = AutoMapperConfig
            .RegisterMappings()
            .CreateMapper();

        services.AddSingleton(mapper);
    }
}
