using Application.Features.AuthFeatures.Commands;
using Application.Features.AuthFeatures.Dtos;
using AutoMapper;
using Core.Entities;

namespace Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCodeCommand, CodeEntity>();
            CreateMap<CodeEntity, CodeDto>();
        }
    }
}
