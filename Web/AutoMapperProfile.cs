using Application.Features.AccountFeatures.Dtos;
using Application.Features.AuthFeatures.Commands;
using Application.Features.CategoryFeatures.Dtos;
using Application.Features.ProductFeatures.Dtos;
using AutoMapper;
using Core.Entities;

namespace Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCodeCommand, CodeEntity>().ForMember(dest => dest.PhoneNumber, act => act.MapFrom(src => src.Phone));
            CreateMap<UserEntity, UserDto>().ForMember(dest => dest.Name, act => act.MapFrom(src => src.PhoneNumber));
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<ProductEntity, ProductDto>();
            CreateMap<FilterValueEntity, FilterValueDto>().ReverseMap();
            CreateMap<FilterNameEntity, FilterNameDto>().ReverseMap();
        }
    }
}
