using MediatR;
using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Paquetes.Queries;

public sealed record GetPaquetesQuery(int? EstadoId) : IRequest<IReadOnlyList<PaqueteListItemResponse>>;