using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwitchCommander.Application.Repositories;
using SwitchCommander.Persistence.Context;
using SwitchCommander.Persistence.Repositories;

namespace SwitchCommander.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<MongoDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}