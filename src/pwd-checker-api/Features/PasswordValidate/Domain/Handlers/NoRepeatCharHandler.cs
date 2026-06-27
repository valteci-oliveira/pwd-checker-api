using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class NoRepeatCharHandler : BaseHandler
    {
        protected override string ValidationMessage => "Password don't must contain repeated characters";

        protected override bool Validate(string password)
        {
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
