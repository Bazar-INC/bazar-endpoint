
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonExceptions;

namespace Application.Features.ProductFeatures.Queries;

public record GetProductFiltersQuery(Guid productId) : IRequest<Dictionary<string, string>>;

public class GetProductFilters : IRequestHandler<GetProductFiltersQuery, Dictionary<string, string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetProductFilters(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Dictionary<string, string>> Handle(GetProductFiltersQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.Products.Get()
            .Include(p => p.Category)
            .Include(p => p.Images)
            .Include(p => p.FilterValues)
            .ThenInclude(f => f.FilterName)
            .AsQueryable().FirstOrDefaultAsync();

        if (product == null)
        {
            throw new BadRequestRestException($"Product with id {request.productId} wasn`t found");
        }

        var filters = new Dictionary<string, string>();

        foreach (var filterValue in product.FilterValues)
        {
            filters.Add(filterValue.FilterName!.Name!, filterValue.Value!);
        }

        return filters;
    }
}