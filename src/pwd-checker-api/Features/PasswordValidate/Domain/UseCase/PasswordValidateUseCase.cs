using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.UseCase
{
    public class PasswordValidateUseCase : IPasswordValidateUseCase
    {
        public PasswordValidateResult Execute(string password)
        {
            
            var result = new PasswordValidateResult
            {
                IsValid = !string.IsNullOrEmpty(password)
            };

            return result;
        }
    }
}