using MediatR;
using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.EstadosPaquete.Queries;

public sealed record ListEstadosPaqueteQuery : IRequest<IReadOnlyList<EstadoPaqueteResponse>>;