using Application.DTO;
using AutoMapper;
using Web.ViewModels;

namespace Web.Mapper
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<ClothingCreateDto, ClothingItemCreateViewModel>()
                .ForMember(p=>p.Price, d=>d.MapFrom(s=>s.ValidPrice))
                .ReverseMap();
            CreateMap<ClothingItemPageDto, ClothingItemPageViewModel>().ReverseMap();
            CreateMap<ClothingAccountDto, ClothingAccountViewModel>()
                .ForMember(p => p.SizeName, d => d.MapFrom(s => s.Size.Name))
                .ForMember(p => p.TypeName, d => d.MapFrom(s => s.Type.Name))
                .ReverseMap();

            CreateMap<SizeDto, SizeViewModel>().ReverseMap();

            CreateMap<TypeDto, TypeViewModel>().ReverseMap();

            CreateMap<ImageDto, ImageViewModel>().ReverseMap();

            CreateMap<BasketDto, BasketViewModel>().ReverseMap();
            CreateMap<BasketItemDto, BasketItemViewModel>().ReverseMap();
        }
    }
}
