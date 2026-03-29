namespace pwd_checker_api.Features.PasswordValidate.Domain.DTOs
{
    public class HandlerResult
    {
        public bool IsValid { get; set; }
        public string? ResultMessage { get; set; } = null;
    }
}