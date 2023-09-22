using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Persistence.Context;
using SwitchCommander.Persistence.Repositories.Features;

namespace SwitchCommander.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<MongoDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}