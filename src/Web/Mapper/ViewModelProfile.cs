using Application.DTO;
using AutoMapper;
using Web.ViewModels;

namespace Web.Mapper
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<ClothingDto, CreateClothingViewModel>().ReverseMap();
            CreateMap<SizeDto, SizeViewModel>().ReverseMap();
            CreateMap<TypeDto, TypeViewModel>().ReverseMap();
            CreateMap<ImageDto, ImageViewModel>().ReverseMap();
        }
    }
}
