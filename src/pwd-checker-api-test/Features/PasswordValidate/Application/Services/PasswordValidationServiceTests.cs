using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Application.Interfaces;
using pwd_checker_api.Features.PasswordValidate.Application.Services;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api_test.Features.PasswordValidate.Application.Services;

public class PasswordValidationServiceTests
{
    private readonly Mock<IPasswordValidateUseCase> _mockUseCase;
    private readonly Mock<ILogger<PasswordValidateService>> _mockLogger;
    private readonly IPasswordValidateService _service;

    public PasswordValidationServiceTests()
    {
        _mockUseCase = new Mock<IPasswordValidateUseCase>();
        _mockLogger = new Mock<ILogger<PasswordValidateService>>();
        _service = new PasswordValidateService(_mockUseCase.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidRequest_ShouldReturnValid()
    {
        var request = new PasswordValidateRequest { Password = "ValidPassword123" };
        var expectedResult = new PasswordValidateResult { IsValid = true, Message = "Password is valid" };
        
        _mockUseCase
            .Setup(u => u.ExecuteAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedResult);
        
        var result = await _service.ExecuteAsync(request);
        
        Assert.NotNull(result);
        Assert.True(result.IsValid);
        _mockUseCase.Verify(u => u.ExecuteAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidPassword_ShouldReturnInvalid()
    {
        var request = new PasswordValidateRequest { Password = "short" };
        var expectedResult = new PasswordValidateResult { IsValid = false, Message = "Password is too short" };
        
        _mockUseCase
            .Setup(u => u.ExecuteAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedResult);
        
        var result = await _service.ExecuteAsync(request);
        
        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.NotNull(result.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithNullRequest_ShouldThrow()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.ExecuteAsync(null!));
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyPassword_ShouldReturnInvalid()
    {
        var request = new PasswordValidateRequest { Password = "" };
        var expectedResult = new PasswordValidateResult { IsValid = false, Message = "Password is empty" };
        
        _mockUseCase
            .Setup(u => u.ExecuteAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedResult);
        
        var result = await _service.ExecuteAsync(request);
        
        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task ExecuteAsync_ResultHasExpectedStructure()
    {
        var request = new PasswordValidateRequest { Password = "ValidPass1" };
        var expectedResult = new PasswordValidateResult { IsValid = true, Message = "Valid" };
        
        _mockUseCase
            .Setup(u => u.ExecuteAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedResult);
        
        var result = await _service.ExecuteAsync(request);
        
        Assert.NotNull(result);
        Assert.NotNull(result.Message);
    }
}
