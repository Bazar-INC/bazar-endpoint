
using Application.Features.QuestionFeatures.Dtos;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonExceptions;

namespace Application.Features.QuestionFeatures.Commands;

public record UpdateQuestionAnswerCommand : UpdateQuestionAnswerRequest, IRequest
{
    public Guid OwnerId { get; set; }
}

public class UpdateQuestionAnswerHandler : IRequestHandler<UpdateQuestionAnswerCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuestionAnswerHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateQuestionAnswerCommand request, CancellationToken cancellationToken)
    {
        var questionAnswerId = request.QuestionAnswerId;
        var questionAnswer = await _unitOfWork.QuestionAnswers.Get(f => f.Id == questionAnswerId)
            .Include(f => f.Owner).FirstOrDefaultAsync();

        if (questionAnswer == null)
        {
            throw new BadRequestRestException($"Question with id {questionAnswerId} wasn`t found.");
        }

        if (questionAnswer.Owner!.Id != request.OwnerId)
        {
            throw new BadRequestRestException($"You don`t have permission to edit the Question answer because the other user have created it.");
        }

        questionAnswer.Text = request.Text;
        questionAnswer.IsEdited = true;

        _unitOfWork.QuestionAnswers.Update(questionAnswer);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class UpdateQuestionAnswerCommandValidator : AbstractValidator<UpdateQuestionAnswerCommand>
{
    public UpdateQuestionAnswerCommandValidator()
    {
        RuleFor(f => f.Text).NotNull();
    }
}