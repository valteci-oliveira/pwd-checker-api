using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class NoRepeatCharHandler: BaseHandler
    {  
        protected override bool Validate(string password)
        {
            VALIDATION_MESSAGE = "Password don't must contain repeated characters";

            var seen = new HashSet<char>();
            foreach (var c in password)
            {
                if (!seen.Add(c))
                    return false;
            }
            return true;
        }
    }
}