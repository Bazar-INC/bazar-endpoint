using Application.Features.AdminFeatures.Commands;
using Application.Features.AdminFeatures.Dtos;
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
    public async Task<ActionResult<UploadImageResponse>> UploadImageAsync([FromBody] UploadImageCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpDelete("static/images/delete/fileName={fileName}")]
    public async Task<ActionResult<Unit>> DeleteImageAsync([FromRoute] string fileName)
    {
        return Ok(await _mediator.Send(new DeleteImageCommand(fileName)));
    }
}
