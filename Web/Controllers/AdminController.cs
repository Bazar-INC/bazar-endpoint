using Application.Features.AccountFeatures.Commands;
using Application.Features.AccountFeatures.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.AuthorizeAttributes;
using Web.Controllers.Abstract;

namespace Web.Controllers;

[AuthorizeAdmin]
public class AdminController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("static/images/upload")]
    public async Task<ActionResult<UploadImageResponse>> UploadImageAsync([FromBody] UploadImageRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<UploadImageCommand>(request);
        command.UserId = userId;

        return Ok(await _mediator.Send(command));
    }
}
