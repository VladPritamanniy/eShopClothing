using System.Security.Claims;
using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Core.Options;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ProductPageService : IProductPageService
    {
        private IMapper _mapper;
        private readonly ISizeService _sizeService;
        private readonly ITypeService _typeService;
        private readonly IClothingService _clothingService;
        private readonly IFileService _fileService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ImageOptions Options { get; set; }

        public ProductPageService(
            IMapper mapper, 
            ISizeService sizeService, 
            ITypeService typeService, 
            IOptions<ImageOptions> options, 
            IClothingService clothingService, 
            IFileService fileService, 
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _sizeService = sizeService;
            _typeService = typeService;
            Options = options.Value;
            _clothingService = clothingService;
            _fileService = fileService;
            _userManager = userManager;
        }

        public async Task CreateProduct(CreateClothingViewModel product, ClaimsPrincipal claims)
        {
            var mapped = _mapper.Map<ClothingDto>(product);
            var user = await _userManager.GetUserAsync(claims);
            mapped.OldPrice = mapped.ValidPrice;
            mapped.ApplicationUserId = user!.Id;
            mapped.CreationDate = DateTime.UtcNow;
            var dto = await _fileService.UploadFiles(mapped, product.FormFiles);
            await _clothingService.CreateClothing(dto);
        }

        public async Task<IEnumerable<SizeViewModel>> GetAllSizesClothing()
        {
            var sizeList = await _sizeService.GetAllSizesClothing();
            var mapped = _mapper.Map<IEnumerable<SizeViewModel>>(sizeList);
            return mapped;
        }

        public async Task<IEnumerable<TypeViewModel>> GetAllTypesClothing()
        {
            var typeList = await _typeService.GetAllTypesClothing();
            var mapped = _mapper.Map<IEnumerable<TypeViewModel>>(typeList);
            return mapped;
        }
    }
}
