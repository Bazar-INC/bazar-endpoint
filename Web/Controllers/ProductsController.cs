using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Dtos;
using Application.Features.ProductFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.AuthorizeAttributes;
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
    public async Task<ActionResult<ProductsResponseDto>> GetProductsAsync([FromQuery] GetProductsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProductAsync([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetProductQuery(id)));
    }

    [HttpGet("ids/")]
    public async Task<ActionResult<GetProductsByIdsResponse>> GetProductsByIdsAsync([FromQuery] GetProductsByIdsQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [AuthorizeAdminManagerSeller]
    [HttpPost("add/")]
    public async Task<ActionResult<Unit>> AddProductAsync([FromBody] AddProductCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}
