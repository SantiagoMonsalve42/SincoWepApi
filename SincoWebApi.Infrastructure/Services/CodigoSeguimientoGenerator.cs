using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Infrastructure.Services;

public sealed class CodigoSeguimientoGenerator : ICodigoSeguimientoGenerator
{
    public string Generate()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
    }
}