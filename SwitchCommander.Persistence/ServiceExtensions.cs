using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SwitchCommander.Application.Repositories;
using SwitchCommander.Persistence.Repositories;

namespace SwitchCommander.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("MongoDB"); // Use the appropriate connection string name
        services.AddSingleton<IMongoClient>(sp => new MongoClient(connectionString));
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var databaseName = "YourDatabaseName"; // Change to your actual database name
            return client.GetDatabase(databaseName);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}