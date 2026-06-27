using pwd_checker_api.Features.PasswordValidate.Domain.Handlers;
using pwd_checker_api_test.Helpers;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.Handlers;

public class MinLengthHandlerTests
{
    private readonly MinLengthHandler _handler = new();

    [Fact]
    public async Task ExecuteAsync_WithValidLength_ShouldPassToNextHandler()
    {
        await HandlerTestHelper.AssertPasses(_handler, "ValidPass123!");
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidLength_ShouldFail()
    {
        await HandlerTestHelper.AssertFailsWithMessage(_handler, "short", "short");
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyPassword_ShouldFail()
    {
        await HandlerTestHelper.AssertFails(_handler, "");
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
        await HandlerTestHelper.AssertPasses(_handler, password);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("short")]
    [InlineData("1234567")]
    public async Task ExecuteAsync_WithInvalidLengths_ShouldFail(string password)
    {
        await HandlerTestHelper.AssertFails(_handler, password);
    }
}
