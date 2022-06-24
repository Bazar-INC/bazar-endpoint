using Application.Features.AccountFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Web.AuthorizeAttributes;
using Web.Controllers.Abstract;

namespace Web.Controllers;

[Route("api/Me")]
public class AccountController : BaseController
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("profile")]
    [AuthorizeCustomer]
    public async Task<IActionResult> GetUser()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        return Ok(await _mediator.Send(new GetUserQuery(userId!)));
    }
}
