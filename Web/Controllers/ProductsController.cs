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
}
