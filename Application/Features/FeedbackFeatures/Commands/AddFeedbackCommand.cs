
using Application.Features.FeedbackFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using FluentValidation;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.CommonExceptions;
using static Shared.AppSettings;

namespace Application.Features.FeedbackFeatures.Commands;

public record AddFeedbackCommand : AddFeedbackRequest, IRequest
{
    public Guid OwnerId { get; set; }
}

public class AddFeedbackHandler : IRequestHandler<AddFeedbackCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;

    public AddFeedbackHandler(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(AddFeedbackCommand request, CancellationToken cancellationToken)
    {
        var userId = request.OwnerId.ToString();
        var owner = await _userManager.FindByIdAsync(userId);

        if(owner == null)
        {
            throw new BadRequestRestException($"User with id {userId} wasn`t found.");
        }

        var productId = request.ProductId;
        var product = await _unitOfWork.Products.FindAsync(productId);

        if(product == null)
        {
            throw new BadRequestRestException($"Product with id {productId} wasn`t found.");
        }

        var feedbackEntity = _mapper.Map<FeedbackEntity>(request);

        await _unitOfWork.Feedbacks.InsertAsync(feedbackEntity);
        await _unitOfWork.SaveChangesAsync();

        return await Unit.Task;
    }
}

public class AddFeedbackCommandValidator : AbstractValidator<AddFeedbackCommand>
{
    public AddFeedbackCommandValidator()
    {
        RuleFor(f => f.Stars).GreaterThanOrEqualTo(Constants.MinStarsCount).LessThanOrEqualTo(Constants.MaxStarsCount);
        RuleFor(f => f.Text).NotNull();
    }
}