using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using pwd_checker_api;
using pwd_checker_api.Features.PasswordValidate.Application.DTOs;

namespace pwd_checker_api_test.Integration;

public class ProgramTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public ProgramTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public void Application_StartsSuccessfully()
    {
        var client = _factory.CreateClient();

        Assert.NotNull(client);
    }

    [Fact]
    public async Task Application_HealthCheckReturnsHealthy()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Application_PasswordValidateEndpointIsAccessible()
    {
        var client = _factory.CreateClient();
        var request = new PasswordValidateRequest { Password = "Abc123@#" };

        var response = await client.PostAsJsonAsync("/api/v1/password/validate", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Application_CorsHeadersArePresent()
    {
        var client = _factory.CreateClient();
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "/api/v1/password/validate");
        httpRequest.Headers.Add("Origin", "http://example.com");
        httpRequest.Content = JsonContent.Create(new PasswordValidateRequest { Password = "Abc123@#" });

        var response = await client.SendAsync(httpRequest);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Application_EndToEnd_ValidPassword()
    {
        var client = _factory.CreateClient();
        var request = new PasswordValidateRequest { Password = "Abc123@#" };

        var response = await client.PostAsJsonAsync("/api/v1/password/validate", request);
        var result = await response.Content.ReadFromJsonAsync<PasswordValidateResult>();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task Application_EndToEnd_InvalidPassword()
    {
        var client = _factory.CreateClient();
        var request = new PasswordValidateRequest { Password = "short" };

        var response = await client.PostAsJsonAsync("/api/v1/password/validate", request);
        var result = await response.Content.ReadFromJsonAsync<PasswordValidateResult>();

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        Assert.NotNull(result);
        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("Abc123@#", HttpStatusCode.OK)]
    [InlineData("short", HttpStatusCode.UnprocessableEntity)]
    [InlineData("", HttpStatusCode.BadRequest)]
    public async Task Application_EndToEnd_VariousPasswords(string password, HttpStatusCode expectedStatus)
    {
        var client = _factory.CreateClient();
        var request = new PasswordValidateRequest { Password = password };

        var response = await client.PostAsJsonAsync("/api/v1/password/validate", request);

        Assert.Equal(expectedStatus, response.StatusCode);
    }
}
