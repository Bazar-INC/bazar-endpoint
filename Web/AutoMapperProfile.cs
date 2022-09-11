using Application.Features.AccountFeatures.Commands;
using Application.Features.AccountFeatures.Dtos;
using Application.Features.AuthFeatures.Commands;
using Application.Features.CategoryFeatures.Commands;
using Application.Features.CategoryFeatures.Dtos;
using Application.Features.FeedbackFeatures.Commands;
using Application.Features.FeedbackFeatures.Dtos;
using Application.Features.FiltersFeatures.Dtos;
using Application.Features.ProductFeatures.Commands;
using Application.Features.ProductFeatures.Dtos;
using Application.Features.QuestionFeatures.Commands;
using Application.Features.QuestionFeatures.Dtos;
using Application.Features.TownFeatures.Dtos;
using AutoMapper;
using Core.Entities;
using static Shared.AppSettings;

namespace Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        /*                          Codes                             */
        CreateMap<AddCodeCommand, CodeEntity>().ForMember(dest => dest.PhoneNumber, act => act.MapFrom(src => src.Phone));

        /*                          Users                             */
        CreateMap<UserEntity, UserDto>()
            .ForMember(dest => dest.Name, act => act.MapFrom(src => CountryCodes.Ukraine + src.PhoneNumber))
            .ForMember(dest => dest.Image, act => act.MapFrom(src => Path.GetFileName(src.Image)));
        CreateMap<SetAvatarRequest, SetAvatarCommand>();

        /*                          Categories                             */
        CreateMap<CategoryEntity, CategoryDto>().
            ForMember(dest => dest.ParentCode, act => act.MapFrom(src => src.Parent == null ? null : src.Parent.Code))
            .ForMember(dest => dest.Image, act => act.MapFrom(src => Path.GetFileName(src.Image)))
            .ForMember(dest => dest.Icon, act => act.MapFrom(src => Path.GetFileName(src.Icon)));

        CreateMap<AddCategoryCommand, CategoryEntity>();

        /*                          Products                             */
        CreateMap<AddProductCommand, ProductEntity>();
        CreateMap<EditProductCommand, ProductEntity>();
        CreateMap<AddProductImageRequest, AddProductImageCommand>();

        CreateMap<ProductEntity, ProductDto>()
            .ForMember(dest => dest.CategoryName, act => act.MapFrom(src => src.Category!.Name));

        /*                          Filters                             */
        CreateMap<FilterValueEntity, FilterValueDto>();
        CreateMap<FilterNameEntity, FilterDto>();
        CreateMap<FilterNameEntity, FilterNameDto>()
            .ForMember(dest => dest.Options, act => act.MapFrom(src => src.FilterValues));

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

        /*                          Towns                           */
        CreateMap<TownEntity, TownDto>();

        /*                          Image                           */
        CreateMap<ImageEntity, ImageDto>()
            .ForMember(dest => dest.Image, act => act.MapFrom(src => Path.GetFileName(src.Path)));

    }
}
