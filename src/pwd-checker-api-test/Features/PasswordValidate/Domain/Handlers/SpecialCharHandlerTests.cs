using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api_test.Helpers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class SpecialCharHandlerTests
{
    private readonly SpecialCharHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithSpecialChar_ShouldPass()
    {
        await HandlerTestHelper.AssertPasses(_handler, "Password123!");
    }

    [Fact]
    public async Task ExecuteAsync_WithoutSpecialChar_ShouldFail()
    {
        await HandlerTestHelper.AssertFailsWithMessage(_handler, "Password123", "special");
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
        await HandlerTestHelper.AssertPasses(_handler, password);
    }

    [Theory]
    [InlineData("Password123")]
    [InlineData("TestPassword")]
    [InlineData("123456")]
    [InlineData("Pwd123")]
    public async Task ExecuteAsync_WithoutSpecialChars_ShouldFail(string password)
    {
        await HandlerTestHelper.AssertFails(_handler, password);
    }
}
