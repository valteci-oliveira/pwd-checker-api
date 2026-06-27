using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api_test.Helpers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class DigitHandlerTests
{
    private readonly DigitHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithDigit_ShouldPass()
    {
        await HandlerTestHelper.AssertPasses(_handler, "password123");
    }

    [Fact]
    public async Task ExecuteAsync_WithoutDigit_ShouldFail()
    {
        await HandlerTestHelper.AssertFailsWithMessage(_handler, "passwordonly", "digit");
    }

    [Theory]
    [InlineData("pass0word")]
    [InlineData("123password")]
    [InlineData("password999")]
    [InlineData("p1ssw0rd")]
    public async Task ExecuteAsync_WithDigits_ShouldPass(string password)
    {
        await HandlerTestHelper.AssertPasses(_handler, password);
    }

    [Theory]
    [InlineData("passwordonly")]
    [InlineData("nothinghere")]
    [InlineData("justletters")]
    public async Task ExecuteAsync_WithoutDigits_ShouldFail(string password)
    {
        await HandlerTestHelper.AssertFails(_handler, password);
    }
}
