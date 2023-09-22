using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SwitchCommander.Application.Common.Behaviors;
using SwitchCommander.Application.Features.UserFeatures.CreateUser;
using SwitchCommander.Application.Features.UserFeatures.ReadUser;

namespace SwitchCommander.Application;

public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddSingleton<CreateUserMapper>();
        services.AddSingleton<ReadUserMapper>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}