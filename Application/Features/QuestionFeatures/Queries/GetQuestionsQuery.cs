
using Application.Features.QuestionFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using Infrastructure.UnitOfWork.Abstract;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.CommonUtils;
using static Shared.AppSettings;

namespace Application.Features.QuestionFeatures.Queries;

public record GetQuestionsQuery : IRequest<GetQuestionsResponse>
{
    public int? Page { get; set; }
    public int? PerPage { get; set; }
    public string? SearchString { get; set; }
}

public class GetQuestionsHandler : IRequestHandler<GetQuestionsQuery, GetQuestionsResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetQuestionsHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<GetQuestionsResponse> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = _unitOfWork.Questions.Get()
             .Include(f => f.Owner)
             .Include(f => f.Product)
             .Include(e => e.Answers)
             .ThenInclude(e => e.Owner)
             .AsQueryable();

        questions = FilterQuestionsBySearchString(questions, request.SearchString);

        questions = CommonUtils.Paginate(
            questions,
            request.PerPage ?? Constants.DefaultPerPage,
            request.Page ?? Constants.DefaultPage,
            out int totalPages);

        return Task.FromResult(new GetQuestionsResponse()
        {
            TotalPages = totalPages,
            Questions = _mapper.Map<ICollection<QuestionResponseDto>>(questions)
        });
    }

    private IQueryable<QuestionEntity> FilterQuestionsBySearchString(IQueryable<QuestionEntity> questions, string? searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            return questions;
        }

        var filteredQuestions = questions.Where(p => p.Text!.ToLower().Contains(searchString.ToLower()) ||
        p.Owner!.UserName!.ToLower().Contains(searchString.ToLower()) ||
        p.Product!.Name!.ToLower().Contains(searchString.ToLower()));

        return filteredQuestions;
    }
}