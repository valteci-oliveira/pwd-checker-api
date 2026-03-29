using pwd_checker_api.Features.PasswordValidate.Application.Handlers;

namespace pwd_checker_api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static WebApplication ConfigureFeatures(this WebApplication app)
    {
        const string groupPrefix = "api/v1";

        var apiGroup = app.MapGroup(groupPrefix);

        var passwordGroup = apiGroup.MapGroup("password")
            .WithName("PasswordValidation");

        passwordGroup.MapPost("/validate", PasswordValidateHandler.Handle)
            .WithName("ValidatePassword")
            .WithDescription("Validates a password according to security rules");

        return app;
    }
}
