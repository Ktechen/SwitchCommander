using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
        services.ConfigureApplication();

        services.ConfigureApiBehavior();
        services.ConfigureCorsPolicy();

        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Your API",
                Version = "v1"
            });

            // Configure JWT authentication for Swagger
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter your JWT token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // Use "bearer" for JWT
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition("Bearer", securityScheme);

            var securityRequirement = new OpenApiSecurityRequirement
            {
                { securityScheme, new[] { "Bearer" } }
            };
            c.AddSecurityRequirement(securityRequirement);
        });

        services.AddOpenApiDocument();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey("YourSecretKey"u8.ToArray())
                };
            });

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
            app.UseReDoc(options => { options.Path = "/redoc"; });
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

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseErrorHandler();
        app.UseCors();

        //Net.8
        //app.MapIdentityApi<IdentityUser>();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}