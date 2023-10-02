﻿using Microsoft.Extensions.Configuration;
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
        services.AddScoped<MongoDbContext>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<ISSHCommandRepository, SSHCommandRepository>();
        services.AddTransient<ISSHCommandConfigurationRepository, SSHCommandConfigurationRepository>();
        services.AddTransient<MongoDbContextSeed>();
        services.BuildServiceProvider().GetRequiredService<MongoDbContextSeed>();
    }
}