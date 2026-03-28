using pwd_checker_api.Features.PasswordValidate.Application.Interfaces;
using pwd_checker_api.Features.PasswordValidate.Application.Services;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;
using pwd_checker_api.Features.PasswordValidate.Domain.UseCase;

namespace pwd_checker_api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPasswordValidateServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IPasswordValidateUseCase, PasswordValidateUseCase>();
        services.AddScoped<IPasswordValidateService, PasswordValidateService>();

        return services;
    }
}
