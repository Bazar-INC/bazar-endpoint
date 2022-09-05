using Application.Features.FeedbackFeatures.Commands;
using Application.Features.FeedbackFeatures.Dtos;
using Application.Features.FeedbackFeatures.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.AuthorizeAttributes;
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

    [HttpGet]
    public async Task<ActionResult<GetFeedbacksResponse>> GetFeedbacksAsync([FromQuery] GetFeedbacksQuery query)
    {
        return Ok(await _mediator.Send(query));
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<GetFeedbackByProductResponse>> GetFeedbacksByProductAsync([FromRoute] Guid productId)
    {
        return Ok(await _mediator.Send(new GetFeedbacksByProductQuery(productId)));
    }

    [Authorize]
    [HttpPost("add-feedback/")]
    public async Task<ActionResult<Unit>> AddFeedbackAsync([FromBody] AddFeedbackRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<AddFeedbackCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [AuthorizeAdminManagerSeller]
    [HttpPost("add-feedback-answer/")]
    public async Task<ActionResult<Unit>> AddFeedbackAnswerAsync([FromBody] AddFeedbackAnswerRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<AddFeedbackAnswerCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpPut("edit-feedback/")]
    public async Task<ActionResult<Unit>> EditFeedbackAsync([FromBody] UpdateFeedbackRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<UpdateFeedbackCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [AuthorizeAdminManagerSeller]
    [HttpPut("edit-feedback-answer/")]
    public async Task<ActionResult<Unit>> EditFeedbackAnswerAsync([FromBody] UpdateFeedbackAnswerRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<UpdateFeedbackAnswerCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }
}
