using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using pwd_checker_api.Extensions;

namespace pwd_checker_api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder);

        var app = builder.Build();

        ConfigureApplication(app);

        app.Run();
    }

    private static void ConfigureServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddOpenApi();
        services.AddSwaggerGen();
        services.AddHealthChecks();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
            loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
        });

        services.AddPasswordValidateServices(configuration);

        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? Array.Empty<string>();

        services.AddCors(options =>
        {
            options.AddPolicy("RestrictedCors", policy =>
            {
                if (allowedOrigins.Length > 0)
                {
                    policy.WithOrigins(allowedOrigins)
                          .WithMethods("POST")
                          .AllowAnyHeader();
                }
                else
                {
                    policy.AllowAnyOrigin()
                          .WithMethods("POST")
                          .AllowAnyHeader();
                }
            });
        });

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddFixedWindowLimiter("FixedWindow", limiterOptions =>
            {
                limiterOptions.PermitLimit = 30;
                limiterOptions.Window = TimeSpan.FromMinutes(1);
                limiterOptions.QueueLimit = 0;
            });
        });
    }

    private static void ConfigureApplication(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.DocumentTitle = "Password Checker API - Swagger UI";
                c.RoutePrefix = "swagger";
            });
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseCors("RestrictedCors");
        app.UseRateLimiter();
        app.UseHealthChecks("/health");
        app.ConfigureFeatures();

        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Application started successfully in {Environment} environment", app.Environment.EnvironmentName);
    }
}
