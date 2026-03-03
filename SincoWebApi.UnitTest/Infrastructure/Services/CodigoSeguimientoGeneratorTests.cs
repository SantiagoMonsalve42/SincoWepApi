using SincoWebApi.Infrastructure.Services;

namespace SincoWebApi.UnitTest.Infrastructure.Services;

public sealed class CodigoSeguimientoGeneratorTests
{
    [Fact]
    public void Generate_ReturnsNonEmptyCode()
    {
        var generator = new CodigoSeguimientoGenerator();

        var code = generator.Generate();

        Assert.False(string.IsNullOrWhiteSpace(code));
        Assert.True(code.Length >= 23);
    }

    [Fact]
    public void Generate_ReturnsDifferentValues()
    {
        var generator = new CodigoSeguimientoGenerator();

        var first = generator.Generate();
        var second = generator.Generate();

        Assert.NotEqual(first, second);
    }
}