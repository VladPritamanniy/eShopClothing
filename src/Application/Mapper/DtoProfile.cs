using Application.DTO;
using AutoMapper;
using Core.Entities;

namespace Application.Mapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<Clothing, ClothingAccountDto>().ReverseMap();
            CreateMap<Clothing, ClothingCreateDto>().ReverseMap();
            CreateMap<Size, SizeDto>().ReverseMap();
            CreateMap<Core.Entities.Type, TypeDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
