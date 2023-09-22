using SwitchCommander.Application;
using SwitchCommander.Persistence;
using SwitchCommander.WebAPI.Extensions;

namespace SwitchCommander.WebAPI;

public class Startup
{
    private IConfiguration Configuration { get; }

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
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseErrorHandler();
        app.UseCors();
        app.UseRouting();
        
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}