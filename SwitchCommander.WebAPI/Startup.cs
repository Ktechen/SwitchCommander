using System.Reflection;
using MediatR;
using SwitchCommander.Application;
using SwitchCommander.Persistence;
using SwitchCommander.Persistence.Context;
using SwitchCommander.WebAPI.Extensions;

namespace SwitchCommander.WebAPI;

public class Startup
{
    public IConfiguration Configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigurePersistence(Configuration);
        services.ConfigureApplication();

        services.ConfigureApiBehavior();
        services.ConfigureCorsPolicy();
        
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        var serviceScope = app.ApplicationServices.CreateScope();
        var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
        dataContext?.Database.EnsureCreated();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseErrorHandler();
        app.UseCors();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}