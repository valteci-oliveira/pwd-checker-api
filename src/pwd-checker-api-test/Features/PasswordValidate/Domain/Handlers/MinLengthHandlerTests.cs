using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class MinLengthHandlerTests
{
    private readonly MinLengthHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithValidLength_ShouldPassToNextHandler()
    {
        var password = "ValidPass123!";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidLength_ShouldFail()
    {
        var password = "short";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.Contains("short", result.ResultMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyPassword_ShouldFail()
    {
        var password = "";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithNullPassword_ShouldThrow()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _handler.ExecuteAsync(null!));
    }

    [Theory]
    [InlineData("12345678")]
    [InlineData("exactlyeight")]
    [InlineData("longerpassword")]
    public async Task ExecuteAsync_WithValidLengths_ShouldPass(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("short")]
    [InlineData("1234567")]
    public async Task ExecuteAsync_WithInvalidLengths_ShouldFail(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }
}
