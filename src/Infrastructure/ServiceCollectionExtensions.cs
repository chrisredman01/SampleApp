using Microsoft.Extensions.DependencyInjection;
using SampleApp.Infrastructure.Data;

namespace SampleApp.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString, bool isDevEnvironment)
    {
        services.AddDataServices(connectionString, isDevEnvironment);

        return services;
    }
}
