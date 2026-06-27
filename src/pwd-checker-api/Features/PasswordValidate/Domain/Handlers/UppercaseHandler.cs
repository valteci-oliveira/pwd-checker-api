namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class UppercaseHandler : CharPredicateHandler
    {
        protected override string ValidationMessage => "Password must contain at least one uppercase letter";
        protected override Func<char, bool> CharPredicate => char.IsUpper;
    }
}
