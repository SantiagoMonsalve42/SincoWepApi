using MediatR;
using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Repartidores.Queries;

public sealed record ListRepartidoresQuery : IRequest<IReadOnlyList<RepartidorResponse>>;