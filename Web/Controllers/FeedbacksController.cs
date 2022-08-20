using Application.Features.FeedbackFeatures.Commands;
using Application.Features.FeedbackFeatures.Dtos;
using Application.Features.FeedbackFeatures.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class FeedbacksController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public FeedbacksController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetFeedbacksByProductAsync([FromRoute] Guid productId)
    {
        return Ok(await _mediator.Send(new GetFeedbacksByProductQuery(productId)));
    }

    [Authorize]
    [HttpPost("add-feedback/")]
    public async Task<IActionResult> AddFeedbackAsync([FromBody] AddFeedbackRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<AddFeedbackCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpPost("add-feedback-answer/")]
    public async Task<IActionResult> AddFeedbackAnswerAsync([FromBody] AddFeedbackAnswerRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<AddFeedbackAnswerCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpPatch("edit-feedback/")]
    public async Task<IActionResult> EditFeedbackAsync([FromBody] UpdateFeedbackRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<UpdateFeedbackCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpPatch("edit-feedback-answer/")]
    public async Task<IActionResult> EditFeedbackAnswerAsync([FromBody] UpdateFeedbackAnswerRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<UpdateFeedbackAnswerCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }
}
