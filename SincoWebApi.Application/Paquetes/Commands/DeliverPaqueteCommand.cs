using MediatR;

namespace SincoWebApi.Application.Paquetes.Commands;

public sealed record DeliverPaqueteCommand(int PaqueteId) : IRequest<bool>;