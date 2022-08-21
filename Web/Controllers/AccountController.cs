using Application.Features.AccountFeatures.Commands;
using Application.Features.AccountFeatures.Dtos;
using Application.Features.AccountFeatures.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

[Authorize]
[Route("api/Me")]
public class AccountController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUserAsync()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

        return Ok(await _mediator.Send(new GetUserQuery(userId!)));
    }

    [HttpPatch("avatar/upload/")]
    public async Task<IActionResult> UploadAvatarAsync([FromBody] SetAvatarRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<SetAvatarCommand>(request);
        command.UserId = userId;

        return Ok(await _mediator.Send(command));
    }
}
