using MediatR;
using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Paquetes.Queries;

public sealed record GetPaqueteByIdQuery(int PaqueteId) : IRequest<PaqueteResponse?>;