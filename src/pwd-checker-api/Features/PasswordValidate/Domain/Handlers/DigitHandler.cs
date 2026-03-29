using pwd_checker_api.Features.PasswordValidate.Domain.DTOs;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class DigitHandler : BaseHandler
    {
        public override Task<HandlerResult> ExecuteAsync(string password)
        {
            if (IsValid(password) == false)
            {
                return Task.FromResult(new HandlerResult { IsValid = false, ResultMessage = "Password must contain at least one digit" });
            }

            if (_nextHandler != null)
            {
                return _nextHandler.ExecuteAsync(password);
            }

            return Task.FromResult(new HandlerResult { IsValid = true });
        }

        private static bool IsValid(string password)
        {
            return password.Any(char.IsDigit);
        }
    }
}