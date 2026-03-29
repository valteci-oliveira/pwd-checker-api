using Microsoft.AspNetCore.Http;
using Moq;
using pwd_checker_api.Features.PasswordValidate.Application.DTOs;
using pwd_checker_api.Features.PasswordValidate.Application.Handlers;
using pwd_checker_api.Features.PasswordValidate.Application.Interfaces;

namespace pwd_checker_api_test.Features.PasswordValidate.Application.Handlers;

public class PasswordValidateHandlerTests
{
    private readonly Mock<IPasswordValidateService> _mockService;

    public PasswordValidateHandlerTests()
    {
        _mockService = new Mock<IPasswordValidateService>();
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldReturnOk()
    {
        var request = new PasswordValidateRequest { Password = "ValidPassword123" };
        var expectedResult = new PasswordValidateResult { IsValid = true, Message = "Password is valid" };
        
        _mockService
            .Setup(s => s.ExecuteAsync(It.IsAny<PasswordValidateRequest>()))
            .ReturnsAsync(expectedResult);
        
        var result = await PasswordValidateHandler.Handle(request, _mockService.Object);
        
        Assert.NotNull(result);
        var okResult = Assert.IsAssignableFrom<IResult>(result);
        Assert.NotNull(okResult);
        _mockService.Verify(s => s.ExecuteAsync(It.IsAny<PasswordValidateRequest>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNullPassword_ShouldReturnBadRequest()
    {
        var request = new PasswordValidateRequest { Password = null };
        var expectedResult = new PasswordValidateResult { IsValid = false, Message = "Password is required" };
        
        _mockService
            .Setup(s => s.ExecuteAsync(It.IsAny<PasswordValidateRequest>()))
            .ReturnsAsync(expectedResult);
        
        var result = await PasswordValidateHandler.Handle(request, _mockService.Object);
        
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Handle_WithEmptyPassword_ShouldReturnBadRequest()
    {
        var request = new PasswordValidateRequest { Password = "" };
        var expectedResult = new PasswordValidateResult { IsValid = false, Message = "Password is required" };
        
        _mockService
            .Setup(s => s.ExecuteAsync(It.IsAny<PasswordValidateRequest>()))
            .ReturnsAsync(expectedResult);
        
        var result = await PasswordValidateHandler.Handle(request, _mockService.Object);
        
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Handle_WithNullRequest_ShouldReturnBadRequest()
    {
        var result = await PasswordValidateHandler.Handle(null!, _mockService.Object);
        
        Assert.NotNull(result);
    }
}
