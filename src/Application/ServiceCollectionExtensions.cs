using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleApp.Application.Common.Behaviours;
using System.Reflection;

namespace SampleApp.Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);

        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(cfg =>
        {
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            cfg.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }
}
