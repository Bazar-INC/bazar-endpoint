
using Application.Features.AccountFeatures.Dtos;
using Application.Features.FeedbackFeatures.Dtos;
using Application.Features.ProductFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.CommonUtils;
using static Shared.AppSettings;

namespace Application.Features.FeedbackFeatures.Queries;

public record GetFeedbacksQuery : IRequest<GetFeedbacksResponse>
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public string? SearchString { get; set; }
}

public class GetFeedbacksHandler : IRequestHandler<GetFeedbacksQuery, GetFeedbacksResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFeedbacksHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<GetFeedbacksResponse> Handle(GetFeedbacksQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = _unitOfWork.Feedbacks.Get()
             .Include(f => f.Owner)
             .Include(f => f.Product)
             .Include(e => e.Answers)
             .ThenInclude(e => e.Owner)
             .AsQueryable();

        feedbacks = FilterFeedbacksBySearchString(feedbacks, request.SearchString);

        feedbacks = CommonUtils.Paginate(
            feedbacks,
            request.PerPage ?? Constants.DefaultPerPage,
            request.Page ?? Constants.DefaultPage,
            out int totalPages);

        return Task.FromResult(new GetFeedbacksResponse()
        {
            TotalPages = totalPages,
            Feedbacks = _mapper.Map<ICollection<FeedbackResponseDto>>(feedbacks)
        });
    }

    private IQueryable<FeedbackEntity> FilterFeedbacksBySearchString(IQueryable<FeedbackEntity> feedbacks, string? searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return feedbacks;
        }

        var filteredFeedbacks = feedbacks.Where(p => p.Text!.ToLower().Contains(searchString.ToLower()) ||
        p.Owner!.UserName!.ToLower().Contains(searchString.ToLower()) ||
        p.Product!.Name!.ToLower().Contains(searchString.ToLower()));

        return filteredFeedbacks;
    }
}