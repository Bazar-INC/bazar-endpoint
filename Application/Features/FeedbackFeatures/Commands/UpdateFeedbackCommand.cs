
using AutoMapper;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.CommonExceptions;
using static Shared.AppSettings;

namespace Application.Features.FeedbackFeatures.Commands;
public record UpdateFeedbackCommand : IRequest
{
    public Guid FeedbackId { get; set; }
    public string? Text { get; set; }
    public int Stars { get; set; }
}

public class UpdateFeedbackHandler : IRequestHandler<UpdateFeedbackCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFeedbackHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = await _unitOfWork.Feedbacks.FindAsync(request.FeedbackId);

        if(feedback == null)
        {
            throw new BadRequestRestException($"Feedback with id {request.FeedbackId} wasn`t found.");
        }

        feedback.Stars = request.Stars;
        feedback.Text = request.Text;
        feedback.IsEdited = true;

        _unitOfWork.Feedbacks.Update(feedback);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class UpdateFeedbackCommandValidator : AbstractValidator<UpdateFeedbackCommand>
{
    public UpdateFeedbackCommandValidator()
    {
        RuleFor(f => f.Stars).GreaterThanOrEqualTo(Constants.MinStarsCount).LessThanOrEqualTo(Constants.MaxStarsCount);
        RuleFor(f => f.Text).NotNull();
    }
}