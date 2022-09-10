
using Application.Features.TownFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;

namespace Application.Features.TownFeatures.Queries;

public record GetTownsQuery : IRequest<ICollection<TownDto>>;

public class GetTownsHandler : IRequestHandler<GetTownsQuery, ICollection<TownDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetTownsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<ICollection<TownDto>> Handle(GetTownsQuery request, CancellationToken cancellationToken)
    {
        var towns = _unitOfWork.Towns.Get();

        return Task.FromResult(_mapper.Map<ICollection<TownDto>>(towns));
    }
}