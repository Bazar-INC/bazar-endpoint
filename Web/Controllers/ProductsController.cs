using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Dtos;
using Application.Features.ProductFeatures.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.AuthorizeAttributes;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
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

    [HttpGet("{id}/filters")]
    public async Task<ActionResult<Dictionary<string, string>>> GetProductFiltersAsync([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new GetProductFiltersQuery(id)));
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

    [AuthorizeAdminManagerSeller]
    [HttpPut("edit/")]
    public async Task<ActionResult<Unit>> EditProductAsync([FromBody] EditProductCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [AuthorizeAdminManagerSeller]
    [HttpPut("{productId}/images/add")]
    public async Task<ActionResult<Unit>> AddProductImageAsync([FromRoute] Guid productId, [FromBody] AddProductImageRequest request)
    {
        var command = _mapper.Map<AddProductImageCommand>(request);
        command.ProductId = productId;
        return Ok(await _mediator.Send(command));
    }

    [AuthorizeAdminManagerSeller]
    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<Unit>> DeleteProductAsync([FromRoute] Guid id)
    {
        return Ok(await _mediator.Send(new DeleteProductCommand(id)));
    }
}
