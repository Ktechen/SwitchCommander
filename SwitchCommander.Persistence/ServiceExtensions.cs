using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwitchCommander.Application.Repositories.Features;
using SwitchCommander.Application.Repositories.Features.SSH;
using SwitchCommander.Persistence.Context;
using SwitchCommander.Persistence.Repositories.Features;
using SwitchCommander.Persistence.Repositories.Features.SSH;
using SwitchCommander.Persistence.Seeds;

namespace SwitchCommander.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        AddMongoDb(services);
        AddRepository(services);
        AddIdentityContext(services, configuration);
    }

    private static void AddMongoDb(this IServiceCollection services)
    {
        services.AddScoped<MongoDbContext>();
        services.AddTransient<MongoDbContextSeed>();
        services.BuildServiceProvider().GetRequiredService<MongoDbContextSeed>();
    }

    private static void AddIdentityContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentityCore<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddEntityFrameworkStores<IdentityContext>();
    }

    private static void AddRepository(this IServiceCollection services)
    {
        services.AddTransient<IUserMongoRepository, UserMongoRepository>();
        services.AddTransient<ISshCommandMongoRepository, SshCommandMongoRepository>();
        services.AddTransient<ISshCommandConfigurationMongoRepository, SshCommandConfigurationMongoRepository>();
        services.AddTransient<ISshServerMongoRepository, SshServerMongoRepository>();
    }
}