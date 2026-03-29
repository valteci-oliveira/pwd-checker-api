using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class UppercaseHandler: BaseHandler
    {   
        protected override bool Validate(string password)
        {
            VALIDATION_MESSAGE = "Password must contain at least one uppercase letter";

            return password.Any(char.IsUpper);
        }
    }
}