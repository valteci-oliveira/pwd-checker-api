using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Application.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Application.Handlers;

public static class PasswordValidateHandler
{
    public static async Task<IResult> Handle(
        PasswordValidateRequest request,
        IPasswordValidateService service,
        ILogger<IPasswordValidateService> logger)
    {
        if (request == null || string.IsNullOrWhiteSpace(request.Password))
        {
            return Results.BadRequest(
                new PasswordValidateResult { 
                    IsValid = false,
                    Message = "Password is required." });
        }

        try
        {
            var result = await service.ExecuteAsync(request);
            
            if(result.IsValid == false)
            {
                return Results.UnprocessableEntity(result);
            }

            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during password validation");
            return Results.Json(
                new PasswordValidateResult
                {
                    IsValid = false,
                    Message = "An internal error occurred while validating the password."
                },
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}