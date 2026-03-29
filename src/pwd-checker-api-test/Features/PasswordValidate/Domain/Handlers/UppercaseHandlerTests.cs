using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class UppercaseHandlerTests
{
    private readonly UppercaseHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithUppercase_ShouldPass()
    {
        var password = "Password123abc";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithoutUppercase_ShouldFail()
    {
        var password = "password123";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.Contains("uppercase", result.ResultMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("Password123")]
    [InlineData("ABC123xyz")]
    [InlineData("Test@password")]
    [InlineData("A")]
    public async Task ExecuteAsync_WithUppercases_ShouldPass(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("password")]
    [InlineData("123456")]
    [InlineData("lowercase")]
    [InlineData("!@#$%^")]
    public async Task ExecuteAsync_WithoutUppercases_ShouldFail(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }
}
