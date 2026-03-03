using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.EstadosPaquete.Queries;

namespace SincoWebApi.UnitTest.Application.EstadosPaquete.Queries;

public sealed class ListEstadosPaqueteQueryTests
{
    [Fact]
    public void ListEstadosPaqueteQuery_ShouldImplementExpectedRequest()
    {
        var query = new ListEstadosPaqueteQuery();

        Assert.IsAssignableFrom<IRequest<IReadOnlyList<EstadoPaqueteResponse>>>(query);
    }
}