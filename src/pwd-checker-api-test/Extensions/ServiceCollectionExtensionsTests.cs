using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using pwd_checker_api.Extensions;
using pwd_checker_api.Features.PasswordValidate.Application.Interfaces;
using pwd_checker_api.Features.PasswordValidate.Domain.Interfaces;

namespace pwd_checker_api_test.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddPasswordValidateServices_RegistersPasswordValidateUseCase()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        services.AddLogging();

        services.AddPasswordValidateServices(configuration);

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var useCase = scope.ServiceProvider.GetService<IPasswordValidateUseCase>();

        Assert.NotNull(useCase);
    }

    [Fact]
    public void AddPasswordValidateServices_RegistersPasswordValidateService()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();
        services.AddLogging();

        services.AddPasswordValidateServices(configuration);

        var provider = services.BuildServiceProvider();
        using var scope = provider.CreateScope();
        var service = scope.ServiceProvider.GetService<IPasswordValidateService>();

        Assert.NotNull(service);
    }

    [Fact]
    public void AddPasswordValidateServices_ReturnsServiceCollection()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        var result = services.AddPasswordValidateServices(configuration);

        Assert.Same(services, result);
    }

    [Fact]
    public void AddPasswordValidateServices_RegistersAsScopedLifetime()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        services.AddPasswordValidateServices(configuration);

        var useCaseDescriptor = services.FirstOrDefault(
            d => d.ServiceType == typeof(IPasswordValidateUseCase));
        var serviceDescriptor = services.FirstOrDefault(
            d => d.ServiceType == typeof(IPasswordValidateService));

        Assert.NotNull(useCaseDescriptor);
        Assert.Equal(ServiceLifetime.Scoped, useCaseDescriptor.Lifetime);
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(ServiceLifetime.Scoped, serviceDescriptor.Lifetime);
    }
}
