using MudBlazor.Services;
using SwitchCommander.Blazor.Services;

namespace SwitchCommander.Blazor;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddServerSideBlazor();
        services.AddTransient<HttpClient>();
        services.AddScoped<ISSHServerClient, SSHServerClient>();
        services.AddScoped<ISSHExecuteCommandClient, SSHExecuteCommandClient>();
        services.AddScoped<ISSHConfigurationClient, SSHConfigurationClient>();
        services.AddScoped<ISSHCommandClient, SSHCommandClient>();
        services.AddMudServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Authentication should be added here if needed
        // app.UseAuthentication();
        // app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapBlazorHub();
            endpoints.MapFallbackToPage("/_Host");
        });
    }
}