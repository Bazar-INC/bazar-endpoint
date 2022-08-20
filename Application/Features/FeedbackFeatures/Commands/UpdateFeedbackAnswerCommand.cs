
using Application.Features.FeedbackFeatures.Dtos;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonExceptions;

namespace Application.Features.FeedbackFeatures.Commands;
public record UpdateFeedbackAnswerCommand : UpdateFeedbackAnswerRequest, IRequest
{
    public Guid OwnerId { get; set; }
}

public class UpdateFeedbackAnswerHandler : IRequestHandler<UpdateFeedbackAnswerCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFeedbackAnswerHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateFeedbackAnswerCommand request, CancellationToken cancellationToken)
    {
        var feedbackAnswerId = request.FeedbackAnswerId;
        var feedbackAnswer = await _unitOfWork.FeedbackAnswers.Get(f => f.Id == feedbackAnswerId)
            .Include(f => f.Owner).FirstOrDefaultAsync();

        if (feedbackAnswer == null)
        {
            throw new BadRequestRestException($"Feedback with id {feedbackAnswerId} wasn`t found.");
        }

        if(feedbackAnswer.Owner!.Id != request.OwnerId)
        {
            throw new BadRequestRestException($"You don`t have permission to edit the feedback answer because the other user have created it.");
        }

        feedbackAnswer.Text = request.Text;
        feedbackAnswer.IsEdited = true;

        _unitOfWork.FeedbackAnswers.Update(feedbackAnswer);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class UpdateFeedbackAnswerCommandValidator : AbstractValidator<UpdateFeedbackAnswerCommand>
{
    public UpdateFeedbackAnswerCommandValidator()
    {
        RuleFor(f => f.Text).NotNull();
    }
}