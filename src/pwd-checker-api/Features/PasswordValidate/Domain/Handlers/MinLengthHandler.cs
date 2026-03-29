using pwd_checker_api.Features.PasswordValidate.Domain.DTOs;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class MinLengthHandler : BaseHandler
    {
        private static readonly int MIN_LENGTH = 8;
        private const string VALIDATION_MESSAGE = "Password is too short";
        public override Task<HandlerResult> ExecuteAsync(string password)
        {
            if (IsValid(password) == false)
            {
                return Task.FromResult(new HandlerResult { IsValid = false, ResultMessage = VALIDATION_MESSAGE });
            }

            if (_nextHandler != null)
            {
                return _nextHandler.ExecuteAsync(password);
            }

            return Task.FromResult(new HandlerResult { IsValid = true });
        }

        private static bool IsValid(string password)
        {
            return password.Length >= MIN_LENGTH;
        }
    }
}