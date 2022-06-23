using Application.Features.AuthFeatures.Commands;
using AutoMapper;
using Core.Entities;

namespace Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCodeCommand, CodeEntity>();
        }
    }
}
