using MediatR;
using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Prioridades.Queries;

public sealed record ListPrioridadesQuery : IRequest<IReadOnlyList<PrioridadResponse>>;