using Application.DTO;
using AutoMapper;
using Web.ViewModels;

namespace Web.Mapper
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<ClothingCreateDto, ClothingCreateViewModel>().ReverseMap();
            CreateMap<SizeDto, SizeViewModel>().ReverseMap();
            CreateMap<TypeDto, TypeViewModel>().ReverseMap();
            CreateMap<ImageDto, ImageViewModel>().ReverseMap();
            CreateMap<ClothingAccountDto, ClothingAccountViewModel>().ReverseMap()
                .ForPath(p=>p.Size.Name, d=>d.MapFrom(s=>s.SizeName))
                .ForPath(p => p.Type.Name, d => d.MapFrom(s => s.TypeName));
        }
    }
}
