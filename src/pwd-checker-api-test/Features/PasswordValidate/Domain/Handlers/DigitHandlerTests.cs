using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class DigitHandlerTests
{
    private readonly DigitHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithDigit_ShouldPass()
    {
        var password = "password123";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithoutDigit_ShouldFail()
    {
        var password = "passwordonly";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.Contains("digit", result.ResultMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("pass0word")]
    [InlineData("123password")]
    [InlineData("password999")]
    [InlineData("p1ssw0rd")]
    public async Task ExecuteAsync_WithDigits_ShouldPass(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("passwordonly")]
    [InlineData("nothinghere")]
    [InlineData("justletters")]
    public async Task ExecuteAsync_WithoutDigits_ShouldFail(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }
}
