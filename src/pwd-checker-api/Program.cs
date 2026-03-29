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
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
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
        app.UseCors("AllowAll");
        app.UseHealthChecks("/health");
        app.ConfigureFeatures();

        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Application started successfully in {Environment} environment", app.Environment.EnvironmentName);
    }
}
