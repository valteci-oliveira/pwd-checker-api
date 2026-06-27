namespace pwd_checker_api.Features.PasswordValidate.Domain.Handlers
{
    public class DigitHandler : CharPredicateHandler
    {
        protected override string ValidationMessage => "Password must contain at least one digit";
        protected override Func<char, bool> CharPredicate => char.IsDigit;
    }
}
