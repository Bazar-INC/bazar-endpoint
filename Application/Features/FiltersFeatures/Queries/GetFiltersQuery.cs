
using Application.Features.FiltersFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonUtils;
using static Shared.AppSettings;

namespace Application.Features.FiltersFeatures.Queries;

public class GetFiltersQuery : IRequest<GetFiltersResponse>
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public string? SearchString { get; set; }
}

public class GetFiltersHandler : IRequestHandler<GetFiltersQuery, GetFiltersResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetFiltersHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<GetFiltersResponse> Handle(GetFiltersQuery request, CancellationToken cancellationToken)
    {
        var filters = _unitOfWork.FilterNames.Get()
            .Include(f => f.FilterValues)
            .Include(f => f.Category)
            .AsQueryable();

        filters = FilterFiltersBySearchString(filters, request.SearchString);

        filters = CommonUtils.Paginate(
            filters,
            request.PerPage ?? Constants.DefaultPerPage,
            request.Page ?? Constants.DefaultPage,
            out int totalPages);

        return Task.FromResult(new GetFiltersResponse()
        {
            Filters = _mapper.Map<ICollection<FilterDto>>(filters),
            TotalPages = totalPages
        });
    }

    private IQueryable<FilterNameEntity> FilterFiltersBySearchString(IQueryable<FilterNameEntity> filters, string? searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return filters;
        }

        var filteredFilters = filters.Where(p => p.Name!.ToLower().Contains(searchString.ToLower()) ||
        p.Code!.ToLower().Contains(searchString.ToLower()) ||
        p.FilterValues.Any(f => f.Value!.ToLower().Contains(searchString.ToLower()) ||
        f.Code!.ToLower().Contains(searchString.ToLower())));

        return filteredFilters;
    }
}