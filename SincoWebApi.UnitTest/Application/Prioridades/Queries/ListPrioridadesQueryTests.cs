using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Prioridades.Queries;

namespace SincoWebApi.UnitTest.Application.Prioridades.Queries;

public sealed class ListPrioridadesQueryTests
{
    [Fact]
    public void ListPrioridadesQuery_ShouldImplementExpectedRequest()
    {
        var query = new ListPrioridadesQuery();

        Assert.IsAssignableFrom<IRequest<IReadOnlyList<PrioridadResponse>>>(query);
    }
}