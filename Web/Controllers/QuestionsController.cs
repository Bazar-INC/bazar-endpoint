using Application.Features.QuestionFeatures.Commands;
using Application.Features.QuestionFeatures.Dtos;
using Application.Features.QuestionFeatures.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class QuestionsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public QuestionsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetQuestionsByProductAsync([FromRoute] Guid productId)
    {
        return Ok(await _mediator.Send(new GetQuestionsByProductQuery(productId)));
    }

    [Authorize]
    [HttpPost("add-question/")]
    public async Task<IActionResult> AddQuestionAsync([FromBody] AddQuestionRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<AddQuestionCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpPost("add-question-answer/")]
    public async Task<IActionResult> AddQuestionAnswerAsync([FromBody] AddQuestionAnswerRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<AddQuestionAnswerCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }

    [Authorize]
    [HttpPatch("edit-question/")]
    public async Task<IActionResult> EditQuestionAsync([FromBody] UpdateQuestionRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<UpdateQuestionCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }
}
