using pwd_checker_api.Features.PasswordValidate.Domain.DTOs;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Interfaces
{
    public abstract class BaseHandler()
    {
        private BaseHandler? _nextHandler;

        protected abstract string ValidationMessage { get; }

        public BaseHandler SetNext(BaseHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return _nextHandler;
        }

        public virtual Task<HandlerResult> ExecuteAsync(string password)
        {
            if (Validate(password) == false)
            {
                return Task.FromResult(new HandlerResult { IsValid = false, ResultMessage = ValidationMessage });
            }

            if (_nextHandler != null)
            {
                return _nextHandler.ExecuteAsync(password);
            }

            return Task.FromResult(new HandlerResult { IsValid = true });
        }

        protected abstract bool Validate(string password);
    }
}
