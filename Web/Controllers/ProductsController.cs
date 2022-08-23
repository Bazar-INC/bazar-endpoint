using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class ProductsController : BaseController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync([FromQuery] GetProductsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductAsync([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetProductQuery(id)));
    }

    [HttpGet("ids/")]
    public async Task<IActionResult> GetProductsByIdsAsync([FromQuery] GetProductsByIdsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpPost("add/")]
    public async Task<IActionResult> AddProductAsync([FromBody] AddProductCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
