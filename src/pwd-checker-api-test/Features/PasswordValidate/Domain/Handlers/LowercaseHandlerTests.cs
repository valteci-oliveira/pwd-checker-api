using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class LowercaseHandlerTests
{
    private readonly LowercaseHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithLowercase_ShouldPass()
    {
        var password = "password123ABC";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithoutLowercase_ShouldFail()
    {
        var password = "PASSWORD123";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.Contains("lowercase", result.ResultMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("passWord123")]
    [InlineData("aBc123xyz")]
    [InlineData("test@password")]
    [InlineData("a")]
    public async Task ExecuteAsync_WithLowercases_ShouldPass(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("PASSWORD")]
    [InlineData("123456")]
    [InlineData("UPPERCASE")]
    [InlineData("!@#$%^")]
    public async Task ExecuteAsync_WithoutLowercases_ShouldFail(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }
}
