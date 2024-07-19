using Application.DTO;
using AutoMapper;
using Core.Entities;

namespace Application.Mapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<Clothing, ClothingDto>().ReverseMap();
            CreateMap<Size, SizeDto>().ReverseMap();
            CreateMap<Core.Entities.Type, TypeDto>().ReverseMap();
        }
    }
}
