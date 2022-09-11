using Application.Features.FiltersFeatures.Dtos;
using Application.Features.FiltersFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class FiltersController : BaseController
{
    private readonly IMediator _mediator;

    public FiltersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<GetFiltersResponse>> GetFiltersAsync([FromQuery] GetFiltersQuery query)
    {
        return await _mediator.Send(query);
    }
}
