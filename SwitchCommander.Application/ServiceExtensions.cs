using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Riok.Mapperly.Abstractions;
using SwitchCommander.Application.Common.Behaviors;
using SwitchCommander.Application.Common.Services;

namespace SwitchCommander.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddMappersFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddSingleton<IPasswordService, PasswordService>();
    }

    private static void AddMappersFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var mapperTypes = assembly.GetTypes()
            .Where(type => type.IsClass &&
                           !type.IsAbstract && type.GetCustomAttribute<MapperAttribute>() != null);

        foreach (var mapperType in mapperTypes) services.AddSingleton(mapperType);
    }
}