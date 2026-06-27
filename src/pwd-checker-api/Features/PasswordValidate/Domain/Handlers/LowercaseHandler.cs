namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class LowercaseHandler : CharPredicateHandler
    {
        protected override string ValidationMessage => "Password must contain at least one lowercase letter";
        protected override Func<char, bool> CharPredicate => char.IsLower;
    }
}
