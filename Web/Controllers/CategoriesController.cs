using Application.Features.CategoryFeatures.Commands;
using Application.Features.CategoryFeatures.Dtos;
using Application.Features.CategoryFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.AuthorizeAttributes;
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
    public async Task<ActionResult<CategoriesResponseDto>> GetCategoriesAsync()
    {
        return Ok(await _mediator.Send(new GetCategoriesQuery()));
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryByCodeAsync([FromRoute] string code)
    {
        return Ok(await _mediator.Send(new GetCategoryByCodeQuery(code)));
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetCategoryByIdQuery(id)));
    }

    [AuthorizeAdminManager]
    [HttpPost("add/")]
    public async Task<ActionResult<Unit>> AddCategoryAsync([FromBody] AddCategoryCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

}
