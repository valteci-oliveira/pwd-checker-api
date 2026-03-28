using pwd_checker_api.Features.PasswordValidate.Application.DTOs;

namespace pwd_checker_api.Features.PasswordValidate.Application.Interfaces;

public interface IPasswordValidateService
{
    Task<PasswordValidateResult> ExecuteAsync(PasswordValidateRequest request);
}
