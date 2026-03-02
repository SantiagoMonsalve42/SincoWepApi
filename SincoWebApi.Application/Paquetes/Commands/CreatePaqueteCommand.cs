using MediatR;
using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Paquetes.Commands;

public sealed record CreatePaqueteCommand(PaqueteCreateRequest Request) : IRequest<PaqueteResponse>;