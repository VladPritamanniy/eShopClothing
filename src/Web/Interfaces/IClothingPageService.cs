using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IClothingPageService
    {
        Task<ClothingHomeIndexViewModel> GetPageItems(int pageNum, int pageSize, int? typeId, int? sizeId);
        Task<ClothingDetailsPageViewModel> GetProductPageInfo(int id);
    }
}
