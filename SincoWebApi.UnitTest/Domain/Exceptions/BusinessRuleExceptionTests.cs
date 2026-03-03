using SincoWebApi.Domain.Exceptions;

namespace SincoWebApi.UnitTest.Domain.Exceptions;

public sealed class BusinessRuleExceptionTests
{
    [Fact]
    public void Constructor_SetsMessage()
    {
        var exception = new BusinessRuleException("Regla de negocio");

        Assert.Equal("Regla de negocio", exception.Message);
    }

    [Fact]
    public void IsExceptionType()
    {
        var exception = new BusinessRuleException("Error");

        Assert.IsAssignableFrom<Exception>(exception);
    }
}