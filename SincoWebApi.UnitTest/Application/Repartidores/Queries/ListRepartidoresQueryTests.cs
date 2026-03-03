using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Repartidores.Queries;

namespace SincoWebApi.UnitTest.Application.Repartidores.Queries;

public sealed class ListRepartidoresQueryTests
{
    [Fact]
    public void ListRepartidoresQuery_ShouldImplementExpectedRequest()
    {
        var query = new ListRepartidoresQuery();

        Assert.IsAssignableFrom<IRequest<IReadOnlyList<RepartidorResponse>>>(query);
    }
}