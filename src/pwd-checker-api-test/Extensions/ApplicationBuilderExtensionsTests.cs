using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using pwd_checker_api;
using pwd_checker_api.Features.PasswordValidate.Application.DTOs;

namespace pwd_checker_api_test.Extensions;

public class ApplicationBuilderExtensionsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApplicationBuilderExtensionsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ConfigureFeatures_MapsPasswordValidateEndpoint()
    {
        var request = new PasswordValidateRequest { Password = "Abc123@#" };

        var response = await _client.PostAsJsonAsync("/api/v1/password/validate", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ConfigureFeatures_PasswordValidateReturnsUnprocessableForInvalidPassword()
    {
        var request = new PasswordValidateRequest { Password = "short" };

        var response = await _client.PostAsJsonAsync("/api/v1/password/validate", request);

        Assert.Equal(HttpStatusCode.UnprocessableEntity, response.StatusCode);
    }

    [Fact]
    public async Task ConfigureFeatures_HealthCheckEndpointIsAvailable()
    {
        var response = await _client.GetAsync("/health");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task ConfigureFeatures_NonExistentEndpointReturns404()
    {
        var response = await _client.GetAsync("/api/v1/nonexistent");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task ConfigureFeatures_ValidPasswordReturnsExpectedBody()
    {
        var request = new PasswordValidateRequest { Password = "Abc123@#" };

        var response = await _client.PostAsJsonAsync("/api/v1/password/validate", request);
        var result = await response.Content.ReadFromJsonAsync<PasswordValidateResult>();

        Assert.NotNull(result);
        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ConfigureFeatures_InvalidPasswordReturnsExpectedBody()
    {
        var request = new PasswordValidateRequest { Password = "short" };

        var response = await _client.PostAsJsonAsync("/api/v1/password/validate", request);
        var result = await response.Content.ReadFromJsonAsync<PasswordValidateResult>();

        Assert.NotNull(result);
        Assert.False(result.IsValid);
        Assert.NotNull(result.Message);
    }
}
