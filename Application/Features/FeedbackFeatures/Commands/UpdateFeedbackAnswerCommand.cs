
using AutoMapper;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.CommonExceptions;
using static Shared.AppSettings;

namespace Application.Features.FeedbackFeatures.Commands;
public record UpdateFeedbackAnswerCommand : IRequest
{
    public Guid FeedbackAnswerId { get; set; }
    public string? Text { get; set; }
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
        var feedbackAnswer = await _unitOfWork.FeedbackAnswers.FindAsync(feedbackAnswerId);

        if (feedbackAnswer == null)
        {
            throw new BadRequestRestException($"Feedback with id {feedbackAnswerId} wasn`t found.");
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