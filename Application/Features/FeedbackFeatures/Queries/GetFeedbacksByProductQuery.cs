
using Application.Features.FeedbackFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.FeedbackFeatures.Queries;

public record GetFeedbacksByProductQuery(Guid ProductId) : IRequest<GetFeedbackByProductResponse>;

public class GetFeedbackByProductHandler : IRequestHandler<GetFeedbacksByProductQuery, GetFeedbackByProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFeedbackByProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetFeedbackByProductResponse> Handle(GetFeedbacksByProductQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = _unitOfWork.Feedbacks.Get(f => f.Product!.Id == request.ProductId)
            .Include(f => f.Owner)
            .Include(f => f.Product)
            .Include(e => e.Answers)
            .ThenInclude(e => e.Owner);

        return await Task.FromResult(new GetFeedbackByProductResponse()
        {
            Feedbacks = _mapper.Map<ICollection<FeedbackResponseDto>>(feedbacks)
        });
    }
}