﻿using Application.Features.AuthFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("request")]
    public async Task<IActionResult> RequestAsync([FromBody] AddCodeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("confirm")]
    public async Task<IActionResult> ConfirmAsync([FromBody] ConfirmCodeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

}
