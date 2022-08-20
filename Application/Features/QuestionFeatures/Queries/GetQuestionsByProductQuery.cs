
using Application.Features.QuestionFeatures.Dtos;
using AutoMapper;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.QuestionFeatures.Queries;

public record GetQuestionsByProductQuery(Guid ProductId) : IRequest<GetQuestionsByProductResponse>;

public class GetQuestionsByProductHandler : IRequestHandler<GetQuestionsByProductQuery, GetQuestionsByProductResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetQuestionsByProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetQuestionsByProductResponse> Handle(GetQuestionsByProductQuery request, CancellationToken cancellationToken)
    {
        var questions = _unitOfWork.Questions.Get(f => f.Product!.Id == request.ProductId)
            .Include(f => f.Owner)
            .Include(e => e.Answers)
            .ThenInclude(e => e.Owner);

        return await Task.FromResult(new GetQuestionsByProductResponse()
        {
            Questions = _mapper.Map<ICollection<QuestionResponseDto>>(questions)
        });
    }
}