using pwd_checker_api.Features.PasswordValidate.Domain.DTOs;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class MinLengthHandler : BaseHandler
    {
        private static readonly int MIN_LENGTH = 8;

        protected override bool Validate(string password)
        {
            VALIDATION_MESSAGE = "Password is too short";
            
            return password.Length >= MIN_LENGTH;
        }
    }
}