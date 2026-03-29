using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class SpecialCharHandler: BaseHandler
    {   
        protected override bool Validate(string password)
        {
            VALIDATION_MESSAGE = "Password must contain at least one special character";

            return password.Any(char.IsPunctuation) || password.Any(char.IsSymbol);
        }
    }
}