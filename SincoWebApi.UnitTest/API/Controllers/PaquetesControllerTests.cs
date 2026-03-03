using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Application.Paquetes.Queries;
using SincoWebApi.Domain.Exceptions;
using SincoWepApi.Controllers;
using System.Reflection;

namespace SincoWebApi.UnitTest.API.Controllers;

public sealed class PaquetesControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PaquetesController _controller;

    public PaquetesControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PaquetesController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsOk_WithPaquetes()
    {
        var estadoId = 1;
        var expected = new List<PaqueteListItemResponse>
        {
            new() { PaqueteId = 10, Descripcion = "Caja 1" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetPaquetesQuery>(q => q.EstadoId == estadoId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _controller.Get(estadoId, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IReadOnlyList<PaqueteListItemResponse>>(ok.Value);
        Assert.Single(value);

        _mediatorMock.Verify(
            m => m.Send(It.Is<GetPaquetesQuery>(q => q.EstadoId == estadoId), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenPaqueteExists()
    {
        var id = 7;
        var expected = new PaqueteResponse { PaqueteId = id, Descripcion = "Sobre" };

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetPaqueteByIdQuery>(q => q.PaqueteId == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await _controller.GetById(id, CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsType<PaqueteResponse>(ok.Value);
        Assert.Equal(id, value.PaqueteId);

        _mediatorMock.Verify(
            m => m.Send(It.Is<GetPaqueteByIdQuery>(q => q.PaqueteId == id), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenPaqueteDoesNotExist()
    {
        var id = 99;

        _mediatorMock
            .Setup(m => m.Send(It.Is<GetPaqueteByIdQuery>(q => q.PaqueteId == id), It.IsAny<CancellationToken>()))
            .ReturnsAsync((PaqueteResponse?)null);

        var result = await _controller.GetById(id, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result.Result);

        _mediatorMock.Verify(
            m => m.Send(It.Is<GetPaqueteByIdQuery>(q => q.PaqueteId == id), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction_WhenCommandSucceeds()
    {
        var request = new PaqueteCreateRequest
        {
            Descripcion = "Paquete nuevo",
            Peso = 3.2m,
            PrioridadId = 1
        };

        var created = new PaqueteResponse
        {
            PaqueteId = 25,
            Descripcion = request.Descripcion
        };

        _mediatorMock
            .Setup(m => m.Send(It.Is<CreatePaqueteCommand>(c => c.Request == request), It.IsAny<CancellationToken>()))
            .ReturnsAsync(created);

        var result = await _controller.Create(request, CancellationToken.None);

        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(PaquetesController.GetById), createdResult.ActionName);
        Assert.Equal(created.PaqueteId, createdResult.RouteValues?["id"]);
        Assert.Same(created, createdResult.Value);

        _mediatorMock.Verify(
            m => m.Send(It.Is<CreatePaqueteCommand>(c => c.Request == request), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenBusinessRuleExceptionIsThrown()
    {
        var request = new PaqueteCreateRequest
        {
            Descripcion = "Inválido",
            Peso = 0.5m,
            PrioridadId = 2
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreatePaqueteCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new BusinessRuleException("Regla de negocio"));

        var result = await _controller.Create(request, CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result.Result);
        var mensaje = GetMensajeFromAnonymousObject(badRequest.Value);

        Assert.Equal("Regla de negocio", mensaje);
    }

    [Fact]
    public async Task Assign_ReturnsNoContent_WhenAssignmentSucceeds()
    {
        var paqueteId = 15;
        var request = new PaqueteAssignRequest { RepartidorId = 4 };

        _mediatorMock
            .Setup(m => m.Send(
                It.Is<AssignRepartidorCommand>(c => c.PaqueteId == paqueteId && c.Request == request),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.Assign(paqueteId, request, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Assign_ReturnsNotFound_WhenAssignmentTargetDoesNotExist()
    {
        var paqueteId = 999;
        var request = new PaqueteAssignRequest { RepartidorId = 2 };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AssignRepartidorCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _controller.Assign(paqueteId, request, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Assign_ReturnsBadRequest_WhenBusinessRuleExceptionIsThrown()
    {
        var paqueteId = 20;
        var request = new PaqueteAssignRequest { RepartidorId = 1 };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<AssignRepartidorCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new BusinessRuleException("No se puede asignar"));

        var result = await _controller.Assign(paqueteId, request, CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var mensaje = GetMensajeFromAnonymousObject(badRequest.Value);

        Assert.Equal("No se puede asignar", mensaje);
    }

    [Fact]
    public async Task Deliver_ReturnsNoContent_WhenDeliverySucceeds()
    {
        var paqueteId = 30;

        _mediatorMock
            .Setup(m => m.Send(It.Is<DeliverPaqueteCommand>(c => c.PaqueteId == paqueteId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var result = await _controller.Deliver(paqueteId, CancellationToken.None);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Deliver_ReturnsNotFound_WhenPaqueteDoesNotExist()
    {
        var paqueteId = 1234;

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeliverPaqueteCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var result = await _controller.Deliver(paqueteId, CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Deliver_ReturnsBadRequest_WhenBusinessRuleExceptionIsThrown()
    {
        var paqueteId = 40;

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<DeliverPaqueteCommand>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new BusinessRuleException("No se puede entregar"));

        var result = await _controller.Deliver(paqueteId, CancellationToken.None);

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var mensaje = GetMensajeFromAnonymousObject(badRequest.Value);

        Assert.Equal("No se puede entregar", mensaje);
    }

    private static string? GetMensajeFromAnonymousObject(object? value)
    {
        if (value is null)
        {
            return null;
        }

        var property = value.GetType().GetProperty("mensaje", BindingFlags.Public | BindingFlags.Instance);
        return property?.GetValue(value)?.ToString();
    }
}