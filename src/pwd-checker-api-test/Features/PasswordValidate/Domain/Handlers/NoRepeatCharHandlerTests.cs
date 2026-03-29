using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class NoRepeatCharHandlerTests
{
    private readonly NoRepeatCharHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithoutRepeatedChars_ShouldPass()
    {
        var password = "Abc123@#";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithRepeatedChars_ShouldFail()
    {
        var password = "Passsword123";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.Contains("repeat", result.ResultMessage, StringComparison.OrdinalIgnoreCase);
    }

    [Theory]
    [InlineData("Abc123")]
    [InlineData("Def4@")]
    [InlineData("Ghi#$%")]
    [InlineData("Jkl567")]
    public async Task ExecuteAsync_WithoutRepeatedChars_WithMultiplePasswords_ShouldPass(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("Passsword")]
    [InlineData("Passwordd123")]
    [InlineData("Passs123")]
    [InlineData("Testtt")]
    [InlineData("aabbccdd")]
    public async Task ExecuteAsync_WithRepeatedChars_WithMultiplePasswords_ShouldFail(string password)
    {
        var result = await _handler.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyPassword_ShouldPass()
    {
        var password = "";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithSingleChar_ShouldPass()
    {
        var password = "a";
        
        var result = await _handler.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }
}
