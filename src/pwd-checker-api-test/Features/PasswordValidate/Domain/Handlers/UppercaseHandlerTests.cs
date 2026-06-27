using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api_test.Helpers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class UppercaseHandlerTests
{
    private readonly UppercaseHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithUppercase_ShouldPass()
    {
        await HandlerTestHelper.AssertPasses(_handler, "Password123abc");
    }

    [Fact]
    public async Task ExecuteAsync_WithoutUppercase_ShouldFail()
    {
        await HandlerTestHelper.AssertFailsWithMessage(_handler, "password123", "uppercase");
    }

    [Theory]
    [InlineData("Password123")]
    [InlineData("ABC123xyz")]
    [InlineData("Test@password")]
    [InlineData("A")]
    public async Task ExecuteAsync_WithUppercases_ShouldPass(string password)
    {
        await HandlerTestHelper.AssertPasses(_handler, password);
    }

    [Theory]
    [InlineData("password")]
    [InlineData("123456")]
    [InlineData("lowercase")]
    [InlineData("!@#$%^")]
    public async Task ExecuteAsync_WithoutUppercases_ShouldFail(string password)
    {
        await HandlerTestHelper.AssertFails(_handler, password);
    }
}
