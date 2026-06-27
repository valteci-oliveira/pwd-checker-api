using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api_test.Helpers;

public static class HandlerTestHelper
{
    public static async Task AssertPasses(BaseHandler handler, string password)
    {
        var result = await handler.ExecuteAsync(password);

        Assert.True(result.IsValid);
    }

    public static async Task AssertFails(BaseHandler handler, string password)
    {
        var result = await handler.ExecuteAsync(password);

        Assert.False(result.IsValid);
    }

    public static async Task AssertFailsWithMessage(
        BaseHandler handler,
        string password,
        string expectedMessageSubstring)
    {
        var result = await handler.ExecuteAsync(password);

        Assert.False(result.IsValid);
        Assert.Contains(expectedMessageSubstring, result.ResultMessage, StringComparison.OrdinalIgnoreCase);
    }
}
