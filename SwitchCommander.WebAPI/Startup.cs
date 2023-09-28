using SwitchCommander.Application;
using SwitchCommander.Persistence;
using SwitchCommander.WebAPI.Extensions;

namespace SwitchCommander.WebAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.ConfigurePersistence(Configuration);
        services.ConfigureApplication();

        services.ConfigureApiBehavior();
        services.ConfigureCorsPolicy();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();
        services.AddOpenApiDocument();

        services.AddLogging(builder =>
        {
            builder.AddConsole(); // Add console logging provider
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseReDoc(options =>
            {
                options.Path = "/redoc";
            });
        }
        else
        {
            // Configure error handling middleware for non-development environments here
            app.UseExceptionHandler("/Error");
            app.UseHsts();

            var resultOfKey = Configuration.GetSection("LicenseKey").GetValue<string>("key");
            if (string.Compare(resultOfKey, "ServerIP", StringComparison.Ordinal) != 0)
            {
                Console.WriteLine("Enter a License Key...");
                Environment.Exit(0);
            }
        }

        app.UseErrorHandler();
        app.UseCors();
        app.UseRouting();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}