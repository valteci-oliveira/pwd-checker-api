using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api_test.Helpers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class LowercaseHandlerTests
{
    private readonly LowercaseHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithLowercase_ShouldPass()
    {
        await HandlerTestHelper.AssertPasses(_handler, "password123ABC");
    }

    [Fact]
    public async Task ExecuteAsync_WithoutLowercase_ShouldFail()
    {
        await HandlerTestHelper.AssertFailsWithMessage(_handler, "PASSWORD123", "lowercase");
    }

    [Theory]
    [InlineData("passWord123")]
    [InlineData("aBc123xyz")]
    [InlineData("test@password")]
    [InlineData("a")]
    public async Task ExecuteAsync_WithLowercases_ShouldPass(string password)
    {
        await HandlerTestHelper.AssertPasses(_handler, password);
    }

    [Theory]
    [InlineData("PASSWORD")]
    [InlineData("123456")]
    [InlineData("UPPERCASE")]
    [InlineData("!@#$%^")]
    public async Task ExecuteAsync_WithoutLowercases_ShouldFail(string password)
    {
        await HandlerTestHelper.AssertFails(_handler, password);
    }
}
