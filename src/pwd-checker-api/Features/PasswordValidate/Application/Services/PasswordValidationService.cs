using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Application.Interfaces;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Application.Services;

public class PasswordValidateService : IPasswordValidateService
{
    private readonly IPasswordValidateUseCase _useCase;
    private readonly ILogger<PasswordValidateService> _logger;

    public PasswordValidateService(
        IPasswordValidateUseCase useCase,
        ILogger<PasswordValidateService> logger)
    {
        _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<PasswordValidateResult> ExecuteAsync(PasswordValidateRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var result = await Task.Run(() => _useCase.Execute(request.Password ?? string.Empty));

        _logger.LogInformation(
            "Password validation completed: IsValid={IsValid}", result.IsValid);

        return result;
    }
}
