using pwd_checker_api.Features.PasswordValidate.Application.DTOs;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

public interface IPasswordValidateUseCase
{
    PasswordValidateResult Execute(string password);
}
