using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SincoWebApi.Application;

namespace SincoWebApi.UnitTest.Application;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddApplication_ShouldRegisterMediatR()
    {
        var services = new ServiceCollection();

        services.AddApplication();

        using var provider = services.BuildServiceProvider();
        var mediator = provider.GetService<IMediator>();

        Assert.NotNull(mediator);
    }
}