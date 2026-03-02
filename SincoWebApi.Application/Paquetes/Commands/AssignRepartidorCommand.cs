using MediatR;
using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Paquetes.Commands;

public sealed record AssignRepartidorCommand(int PaqueteId, PaqueteAssignRequest Request) : IRequest<bool>;