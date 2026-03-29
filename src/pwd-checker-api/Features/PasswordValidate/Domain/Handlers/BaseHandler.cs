using pwd_checker_api.Features.PasswordValidate.Domain.DTOs;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Interfaces
{
    public abstract class BaseHandler()
    {
        protected  BaseHandler? _nextHandler;

        public BaseHandler SetNext(BaseHandler nextHandler)
        {
            _nextHandler = nextHandler;
            return _nextHandler;
        }               

        public virtual Task<HandlerResult> ExecuteAsync(string password)
        {
            if (_nextHandler != null)
            {
                return _nextHandler.ExecuteAsync(password);
            }

            return Task.FromResult(new HandlerResult { IsValid = true });
        }
    }
}