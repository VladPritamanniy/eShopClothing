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
            CreateMap<Clothing, ClothingDto>().ReverseMap();
            CreateMap<Clothing, ClothingAccountDto>().ReverseMap();
            CreateMap<Clothing, ClothingCreateDto>().ReverseMap();
            CreateMap<Clothing, ClothingItemPageDto>()
                .ForMember(d => d.ImagePublicId, e => e.MapFrom(m => m.Images.FirstOrDefault()!.PublicId))
                .ForMember(d => d.ValidPrice, e => e.MapFrom(m => m.ValidPrice))
                .ForMember(d => d.OldPrice, e => e.MapFrom(m => m.OldPrice))
                .ForMember(d => d.SizeName, e => e.MapFrom(m => m.Size.Name))
                .ReverseMap();

            CreateMap<Size, SizeDto>().ReverseMap();

            CreateMap<Type, TypeDto>().ReverseMap();

            CreateMap<Image, ImageDto>().ReverseMap();

            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>()
                .ForMember(d => d.Name, e => e.MapFrom(m => m.Clothing.Name))
                .ForMember(d=>d.ValidPrice, e=>e.MapFrom(m=>m.Clothing.ValidPrice))
                .ForMember(d => d.OldPrice, e => e.MapFrom(m => m.Clothing.OldPrice))
                .ForMember(d => d.ImagePublicId, e => e.MapFrom(m => m.Clothing.Images.FirstOrDefault()!.PublicId))
                .ForMember(d => d.SizeName, e => e.MapFrom(m => m.Clothing.Size.Name))
                .ReverseMap();
        }
    }
}
