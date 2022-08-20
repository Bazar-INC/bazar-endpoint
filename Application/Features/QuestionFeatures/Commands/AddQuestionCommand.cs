
using Application.Features.QuestionFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonExceptions;

namespace Application.Features.QuestionFeatures.Commands;

public record AddQuestionCommand : AddQuestionRequest, IRequest
{
    public Guid OwnerId { get; set; }
}
public class AddQuestionHandler : IRequestHandler<AddQuestionCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    public AddQuestionHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(AddQuestionCommand request, CancellationToken cancellationToken)
    {
        var userId = request.OwnerId.ToString();
        var owner = await _userManager.FindByIdAsync(userId);

        if (owner == null)
        {
            throw new BadRequestRestException($"User with id {userId} wasn`t found.");
        }

        var productId = request.ProductId;
        var product = await _unitOfWork.Products.FindAsync(productId);

        if (product == null)
        {
            throw new BadRequestRestException($"Product with id {productId} wasn`t found.");
        }

        var questionEntity = _mapper.Map<QuestionEntity>(request);

        await _unitOfWork.Questions.InsertAsync(questionEntity);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class AddQuestionCommandValidator : AbstractValidator<AddQuestionCommand>
{
    public AddQuestionCommandValidator()
    {
        RuleFor(f => f.Text).NotNull();
    }
}