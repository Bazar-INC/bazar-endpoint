
using Application.Features.QuestionFeatures.Dtos;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonExceptions;

namespace Application.Features.QuestionFeatures.Commands;

public record AddQuestionAnswerCommand : AddQuestionAnswerRequest, IRequest
{
    public Guid OwnerId { get; set; }
}

public class AddQuestionAnswerHandler : IRequestHandler<AddQuestionAnswerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<UserEntity> _userManager;

    public AddQuestionAnswerHandler(IUnitOfWork unitOfWork, UserManager<UserEntity> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(AddQuestionAnswerCommand request, CancellationToken cancellationToken)
    {
        var questionId = request.QuestionId;
        var question = await _unitOfWork.Questions.FindAsync(questionId);

        if (question == null)
        {
            throw new BadRequestRestException($"Question with id {questionId} wasn`t found");
        }

        var userId = request.OwnerId.ToString();
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            throw new BadRequestRestException($"User with id {userId} wasn`t found");
        }

        var questionAnswer = new QuestionAnswerEntity()
        {
            Question = question,
            Owner = user,
            Text = request.Text
        };

        await _unitOfWork.QuestionAnswers.InsertAsync(questionAnswer);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class AddQuestionAnswerCommandValidator : AbstractValidator<AddQuestionAnswerCommand>
{
    public AddQuestionAnswerCommandValidator()
    {
        RuleFor(f => f.Text).NotNull();
    }
}