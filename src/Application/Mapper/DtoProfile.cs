using Application.DTO;
using AutoMapper;
using Core.Entities;
using Type = Core.Entities.Type;

namespace Application.Mapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<Clothing, ClothingAccountDto>().ReverseMap();
            CreateMap<Clothing, ClothingCreateDto>().ReverseMap();
            CreateMap<Clothing, ClothingItemPageDto>()
                .ForMember(d => d.Image, opt => opt.MapFrom(src => src.Images.FirstOrDefault()!.Value))
                .ForMember(d => d.Price, opt => opt.MapFrom(src => src.ValidPrice))
                .ForMember(d => d.SizeName, opt => opt.MapFrom(src => src.Size.Name))
                .ForMember(d => d.TypeName, opt => opt.MapFrom(src => src.Type.Name))
                .ReverseMap();
            CreateMap<Size, SizeDto>().ReverseMap();
            CreateMap<Type, TypeDto>().ReverseMap();
            CreateMap<Image, ImageDto>().ReverseMap();
        }
    }
}
