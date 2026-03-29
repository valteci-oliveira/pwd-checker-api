namespace pwd_checker_api.Features.PasswordValidate.Application.DTOs;

public class PasswordValidateResult
{
    public bool IsValid { get; set; }
    public string? Message { get; set; } = string.Empty;
}
