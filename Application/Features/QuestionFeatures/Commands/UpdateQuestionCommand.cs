
using Application.Features.QuestionFeatures.Dtos;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Shared.CommonExceptions;

namespace Application.Features.QuestionFeatures.Commands;

public record UpdateQuestionCommand : UpdateQuestionRequest, IRequest
{
    public Guid OwnerId { get; set; }
}

public class UpdateQuestionHandler : IRequestHandler<UpdateQuestionCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuestionHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
    {
        var question = await _unitOfWork.Questions.FindAsync(request.QuestionId);

        if (question == null)
        {
            throw new BadRequestRestException($"Question with id {request.QuestionId} wasn`t found.");
        }

        if (question.OwnerId != request.OwnerId)
        {
            throw new BadRequestRestException($"You don`t have permission to edit the question because the other user have created it.");
        }

        question.Text = request.Text;
        question.IsEdited = true;

        _unitOfWork.Questions.Update(question);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
{
    public UpdateQuestionCommandValidator()
    {
        RuleFor(f => f.Text).NotNull();
    }
}
