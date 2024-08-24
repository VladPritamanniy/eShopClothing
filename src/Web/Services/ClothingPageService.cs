using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Services
{
    public class ClothingPageService : IClothingPageService
    {
        private readonly IClothingService _clothingService;
        private readonly IMapper _mapper;
        private readonly ISizeService _sizeService;
        private readonly ITypeService _typeService;

        public ClothingPageService(IClothingService clothingService, IMapper mapper, ISizeService sizeService, ITypeService typeService)
        {
            _clothingService = clothingService;
            _mapper = mapper;
            _sizeService = sizeService;
            _typeService = typeService;
        }

        public async Task<ClothingHomeIndexViewModel> GetPageItems(int pageNum, int pageSize, int? typeId, int? sizeId)
        {
            var paginationFilterDto = new ClothingPaginationFilterDto
            {
                PageNum = pageNum,
                PageSize = pageSize,
                TypeId = typeId,
                SizeId = sizeId
            };

            var itemsOnPage = await _clothingService.GetAllWithPagination(paginationFilterDto);
            var totalItems = await _clothingService.GetCount(paginationFilterDto.TypeId, paginationFilterDto.SizeId);

            var vm = new ClothingHomeIndexViewModel
            {
                ClothingItems = _mapper.Map<List<ClothingItemPageViewModel>>(itemsOnPage),
                Types = await GetTypes(),
                Sizes = await GetSizes(),
                TypeFilterApplied = typeId ?? 0,
                SizeFilterApplied = sizeId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageNum,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / pageSize)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages) ? "btn-secondary is-disabled" : "btn-primary";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 1) ? "btn-secondary is-disabled" : "btn-primary";

            return vm;
        }

        private async Task<List<SelectListItem>> GetTypes()
        {
            var typesDto = await _typeService.GetAllTypesClothing();
            var typesModel = _mapper.Map<List<TypeViewModel>>(typesDto);

            var items = typesModel
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Name })
                .OrderBy(t => t.Text)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }

        private async Task<List<SelectListItem>> GetSizes()
        {
            var sizeDto = await _sizeService.GetAllSizesClothing();
            var sizesModel = _mapper.Map<List<SizeViewModel>>(sizeDto);

            var items = sizesModel
                .Select(type => new SelectListItem() { Value = type.Id.ToString(), Text = type.Name })
                .OrderBy(t => t.Value)
                .ToList();

            var allItem = new SelectListItem() { Value = null, Text = "All", Selected = true };
            items.Insert(0, allItem);

            return items;
        }
    }
}
