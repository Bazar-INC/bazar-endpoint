using Application.Features.QuestionFeatures.Commands;
using Application.Features.QuestionFeatures.Dtos;
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

    [Authorize]
    [HttpPost("add-question/")]
    public async Task<IActionResult> AddQuestionAsync([FromBody] AddQuestionRequest request)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "id")!.Value!);

        var command = _mapper.Map<AddQuestionCommand>(request);
        command.OwnerId = userId;

        return Ok(await _mediator.Send(command));
    }
}
