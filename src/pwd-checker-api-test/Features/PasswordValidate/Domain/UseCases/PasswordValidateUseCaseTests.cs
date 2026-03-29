using Microsoft.Extensions.Logging;
using Moq;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;
using pwd_checker_api.Features.PasswordValidate.Domain.UseCases;

namespace pwd_checker_api_test.Features.PasswordValidate.Domain.UseCases;

public class PasswordValidateUseCaseTests
{
    private readonly Mock<ILogger<PasswordValidateUseCase>> _mockLogger;
    private readonly IPasswordValidateUseCase _useCase;

    public PasswordValidateUseCaseTests()
    {
        _mockLogger = new Mock<ILogger<PasswordValidateUseCase>>();
        _useCase = new PasswordValidateUseCase(_mockLogger.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidPassword_ShouldReturnValid()
    {
        var password = "Abc123@#";
        
        var result = await _useCase.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithShortPassword_ShouldReturnInvalid()
    {
        var password = "short";
        
        var result = await _useCase.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithoutDigit_ShouldReturnInvalid()
    {
        var password = "longenoughpasswordbutnodigit";
        
        var result = await _useCase.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.NotNull(result.Message);
    }

    [Theory]
    [InlineData("Def456@!")]
    [InlineData("Ghi789#$")]
    [InlineData("Jkl345*-")]
    public async Task ExecuteAsync_WithValidPasswords_ShouldReturnValid(string password)
    {
        var result = await _useCase.ExecuteAsync(password);
        
        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("short")]
    [InlineData("invalid")]
    [InlineData("nothisone")]
    [InlineData("")]
    public async Task ExecuteAsync_WithInvalidPasswords_ShouldReturnInvalid(string password)
    {
        var result = await _useCase.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_WithNullPassword_ShouldThrow()
    {
        await Assert.ThrowsAsync<NullReferenceException>(() => _useCase.ExecuteAsync(null!));
    }

    [Fact]
    public async Task ExecuteAsync_ResultMessageIsNotEmpty_WhenInvalid()
    {
        var password = "invalid";
        
        var result = await _useCase.ExecuteAsync(password);
        
        Assert.False(result.IsValid);
        Assert.NotNull(result.Message);
        Assert.NotEmpty(result.Message);
    }
}
