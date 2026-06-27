using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public abstract class CharPredicateHandler : BaseHandler
    {
        protected abstract Func<char, bool> CharPredicate { get; }

        protected override bool Validate(string password)
        {
            return password.Any(CharPredicate);
        }
    }
}
