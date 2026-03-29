using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Application.Interfaces;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Application.Services;

public class PasswordValidateService(
    IPasswordValidateUseCase useCase,
    ILogger<PasswordValidateService> logger) : IPasswordValidateService
{
    private readonly IPasswordValidateUseCase _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
    private readonly ILogger<PasswordValidateService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<PasswordValidateResult> ExecuteAsync(PasswordValidateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var result = await _useCase.ExecuteAsync(request.Password ?? string.Empty);

        _logger.LogInformation("Password validation completed: IsValid={IsValid}", result.IsValid);

        return result;
    }
}
