using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class NoRepeatCharHandler: BaseHandler
    {  
        private const string REGEX_PATTERN = @"(.).*\1"; 
        protected override bool Validate(string password)
        {
            VALIDATION_MESSAGE = "Password don't must contain repeated characters";

            return !Regex.IsMatch(password, REGEX_PATTERN);
        }
    }
}