using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SwitchCommander.Application;
using SwitchCommander.Infrastructure;
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
        services.ConfigureApplication(Configuration);

        services.ConfigureApiBehavior();
        services.ConfigureCorsPolicy();

        services.AddControllers();
        services.AddEndpointsApiExplorer();


        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole("admin");
            });

            options.AddPolicy("StandardUser", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("userType", "standard");
            });
        });

        var secretKey = new SymmetricSecurityKey("superSecretKey@2410"u8.ToArray());
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            "Kev",
            "https://localhost:44317",
            new List<Claim>(),
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        Console.WriteLine(tokenString);

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = "https://localhost:44317",
                    ValidIssuer = "Kev",
                    IssuerSigningKey = secretKey
                };
            });


        services.AddLogging(builder =>
        {
            builder.AddConsole(); // Add console logging provider
        });


        services.ConfigureSwagger();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseOpenApi();

            app.UseSwaggerUI(c =>
            {
                c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 von Swagger");
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
                Console.WriteLine("Enter a License Key ...");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseErrorHandler();
        app.UseCors();
        app.UseHangfireDashboard();

        //Net.8
        //app.MapIdentityApi<IdentityUser>();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHangfireDashboard();
        });
    }
}