using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api_test.Helpers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class NoRepeatCharHandlerTests
{
    private readonly NoRepeatCharHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithoutRepeatedChars_ShouldPass()
    {
        await HandlerTestHelper.AssertPasses(_handler, "Abc123@#");
    }

    [Fact]
    public async Task ExecuteAsync_WithRepeatedChars_ShouldFail()
    {
        await HandlerTestHelper.AssertFailsWithMessage(_handler, "Passsword123", "repeat");
    }

    [Theory]
    [InlineData("Abc123")]
    [InlineData("Def4@")]
    [InlineData("Ghi#$%")]
    [InlineData("Jkl567")]
    public async Task ExecuteAsync_WithoutRepeatedChars_WithMultiplePasswords_ShouldPass(string password)
    {
        await HandlerTestHelper.AssertPasses(_handler, password);
    }

    [Theory]
    [InlineData("Passsword")]
    [InlineData("Passwordd123")]
    [InlineData("Passs123")]
    [InlineData("Testtt")]
    [InlineData("aabbccdd")]
    public async Task ExecuteAsync_WithRepeatedChars_WithMultiplePasswords_ShouldFail(string password)
    {
        await HandlerTestHelper.AssertFails(_handler, password);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyPassword_ShouldPass()
    {
        await HandlerTestHelper.AssertPasses(_handler, "");
    }

    [Fact]
    public async Task ExecuteAsync_WithSingleChar_ShouldPass()
    {
        await HandlerTestHelper.AssertPasses(_handler, "a");
    }
}
