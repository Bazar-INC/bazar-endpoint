using Application.Features.TownFeatures.Dtos;
using Application.Features.TownFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class TownsController : BaseController
{
    private readonly IMediator _mediator;

    public TownsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<TownDto>>> GetTownsAsync()
    {
        return Ok(await _mediator.Send(new GetTownsQuery()));
    }
}
