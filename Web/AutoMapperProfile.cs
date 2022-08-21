using Application.Features.AccountFeatures.Dtos;
using Application.Features.AuthFeatures.Commands;
using Application.Features.CategoryFeatures.Dtos;
using Application.Features.FeedbackFeatures.Commands;
using Application.Features.FeedbackFeatures.Dtos;
using Application.Features.ProductFeatures.Dtos;
using Application.Features.QuestionFeatures.Commands;
using Application.Features.QuestionFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using static Shared.AppSettings;

namespace Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<AddCodeCommand, CodeEntity>().ForMember(dest => dest.PhoneNumber, act => act.MapFrom(src => src.Phone));
        CreateMap<UserEntity, UserDto>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => CountryCodes.Ukraine + src.PhoneNumber));
        CreateMap<CategoryEntity, CategoryDto>().ForMember(dest => dest.ParentCode, act => act.MapFrom(src => src.Parent == null ? null : src.Parent.Code));
        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.Images, act => act.MapFrom(src => src.Images.Select(i => i.Path)))
            .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.Category!.Name));

        /*                          Filters                             */

        CreateMap<FilterValueEntity, FilterValueDto>().ReverseMap();
        CreateMap<FilterNameEntity, FilterNameDto>().ReverseMap();

        /*                          Feedbacks                           */

        CreateMap<FeedbackEntity, FeedbackResponseDto>()
            .ForMember(dest => dest.Answers, act => act.MapFrom(src => src.Answers.OrderBy(a => a.CreatedAt)))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(src => src.CreatedAt.ToString(Formats.CommentDateFormat)));

        CreateMap<FeedbackAnswerEntity, FeedbackAnswerResponseDto>()
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(src => src.CreatedAt.ToString(Formats.CommentDateFormat)));

        CreateMap<AddFeedbackCommand, FeedbackEntity>();
        CreateMap<AddFeedbackRequest, AddFeedbackCommand>();

        CreateMap<AddFeedbackAnswerRequest, AddFeedbackAnswerCommand>();

        CreateMap<UpdateFeedbackRequest, UpdateFeedbackCommand>();
        CreateMap<UpdateFeedbackAnswerRequest, UpdateFeedbackAnswerCommand>();

        /*                          Questions                           */

        CreateMap<QuestionEntity, QuestionResponseDto>()
            .ForMember(dest => dest.Answers, act => act.MapFrom(src => src.Answers.OrderBy(a => a.CreatedAt)))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(src => src.CreatedAt.ToString(Formats.CommentDateFormat)));

        CreateMap<QuestionAnswerEntity, QuestionAnswerResponseDto>()
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(src => src.CreatedAt.ToString(Formats.CommentDateFormat)));

        CreateMap<AddQuestionCommand, QuestionEntity>();
        CreateMap<AddQuestionRequest, AddQuestionCommand>();

        CreateMap<AddQuestionAnswerRequest, AddQuestionAnswerCommand>();

        CreateMap<UpdateQuestionRequest, UpdateQuestionCommand>();
        CreateMap<UpdateQuestionAnswerRequest, UpdateQuestionAnswerCommand>();

    }
}
