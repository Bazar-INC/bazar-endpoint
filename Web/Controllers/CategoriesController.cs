using Application.Features.CategoryFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class CategoriesController : BaseController
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        return Ok(await _mediator.Send(new GetCategoriesQuery()));
    }

    [HttpGet("{code}")]
    public async Task<IActionResult> GetCategoryAsync([FromRoute] string code)
    {
        return Ok(await _mediator.Send(new GetCategoryQuery(code)));
    }
}
