using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class SpecialCharHandlerTests
{
    private readonly SpecialCharHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithSpecialChar_ShouldPass()
    {
        var password = "Password123!";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithoutSpecialChar_ShouldFail()
    {
        var password = "Password123";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.Contains("special", result.ResultMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("Password@123")]
    [InlineData("Test#Pass1")]
    [InlineData("Pwd$123xyz")]
    [InlineData("Valid%Pass")]
    [InlineData("Secure&Pass")]
    [InlineData("Strong*Pwd")]
    [InlineData("Pass-word1")]
    public async Task ExecuteAsync_WithSpecialChars_ShouldPass(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Password123")]
    [InlineData("TestPassword")]
    [InlineData("123456")]
    [InlineData("Pwd123")]
    public async Task ExecuteAsync_WithoutSpecialChars_ShouldFail(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }
}
