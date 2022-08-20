﻿using Application.Features.FeedbackFeatures.Commands;
using Application.Features.FeedbackFeatures.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class FeedbacksController : BaseController
{
    private readonly IMediator _mediator;

    public FeedbacksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetFeedbacksByProductAsync([FromRoute] Guid productId)
    {
        return Ok(await _mediator.Send(new GetFeedbacksByProductQuery(productId)));
    }

    [HttpPost("add-feedback/")]
    public async Task<IActionResult> AddFeedback([FromBody] AddFeedbackCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

}
