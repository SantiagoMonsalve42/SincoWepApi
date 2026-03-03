using SincoWebApi.Domain.Constants;

namespace SincoWebApi.UnitTest.Domain.Constants;

public sealed class EstadoPaqueteIdsTests
{
    [Fact]
    public void Constants_HaveExpectedValues()
    {
        Assert.Equal(1, EstadoPaqueteIds.EnBodega);
        Assert.Equal(2, EstadoPaqueteIds.Asignado);
        Assert.Equal(3, EstadoPaqueteIds.Entregado);
    }
}