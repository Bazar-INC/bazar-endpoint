
using Application.Features.OrderFeatures.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class OrdersController : BaseController
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<ActionResult<Unit>> AddOrderAsync(AddOrderCommand command)
    {
        return await _mediator.Send(command);
    }
}
