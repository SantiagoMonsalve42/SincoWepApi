using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Queries;

namespace SincoWebApi.UnitTest.Application.Paquetes.Queries;

public sealed class PaquetesQueriesTests
{
    [Fact]
    public void GetPaquetesQuery_ShouldStoreEstadoId()
    {
        var query = new GetPaquetesQuery(2);

        Assert.Equal(2, query.EstadoId);
        Assert.IsAssignableFrom<IRequest<IReadOnlyList<PaqueteListItemResponse>>>(query);
    }

    [Fact]
    public void GetPaqueteByIdQuery_ShouldStorePaqueteId()
    {
        var query = new GetPaqueteByIdQuery(123);

        Assert.Equal(123, query.PaqueteId);
        Assert.IsAssignableFrom<IRequest<PaqueteResponse?>>(query);
    }
}