using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application.Common.Interfaces;
using SampleApp.Infrastructure.Data.Interceptors;

namespace SampleApp.Infrastructure.Data;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, string connectionString, bool isDevEnvironment)
    {
        services.AddScoped<ISaveChangesInterceptor, DomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, ChangeTrackingInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            if (isDevEnvironment)
            {
                options.EnableSensitiveDataLogging().EnableDetailedErrors();
            }

            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(s => s.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
