using FileConverter.Worker.Logic.Services;
using FileConverter.Worker.Protocol;
using Microsoft.Extensions.DependencyInjection;

namespace FileConverter.Worker.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<KafkaEventSource<ImageConversionTask>>();
        
        return services;
    }
}