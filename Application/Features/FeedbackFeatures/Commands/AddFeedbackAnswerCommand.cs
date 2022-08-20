using Application.Features.FeedbackFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonExceptions;

namespace Application.Features.FeedbackFeatures.Commands;

public record AddFeedbackAnswerCommand : AddFeedbackAnswerRequest, IRequest
{
    public Guid OwnerId { get; set; }
}

public class AddFeedbackAnswerHandler : IRequestHandler<AddFeedbackAnswerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<UserEntity> _userManager;

    public AddFeedbackAnswerHandler(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(AddFeedbackAnswerCommand request, CancellationToken cancellationToken)
    {
        var feedbackId = request.FeedbackId;
        var feedback = await _unitOfWork.Feedbacks.FindAsync(feedbackId);

        if(feedback == null)
        {
            throw new BadRequestRestException($"Feedback with id {feedbackId} wasn`t found");
        }

        var userId = request.OwnerId.ToString();
        var user = await _userManager.FindByIdAsync(userId);

        if(user == null)
        {
            throw new BadRequestRestException($"User with id {userId} wasn`t found");
        }

        var feedbackAnswer = new FeedbackAnswerEntity()
        {
            Feedback = feedback,
            Owner = user,
            Text = request.Text
        };

        await _unitOfWork.FeedbackAnswers.InsertAsync(feedbackAnswer);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class AddFeedbackAnswerCommandValidator : AbstractValidator<AddFeedbackAnswerCommand>
{
    public AddFeedbackAnswerCommandValidator()
    {
        RuleFor(f => f.Text).NotNull();
    }
}