namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class SpecialCharHandler : CharPredicateHandler
    {
        protected override string ValidationMessage => "Password must contain at least one special character";
        protected override Func<char, bool> CharPredicate => c => char.IsPunctuation(c) || char.IsSymbol(c);
    }
}
