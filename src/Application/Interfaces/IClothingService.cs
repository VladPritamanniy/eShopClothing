using Application.DTO;

namespace Application.Interfaces
{
    public interface IClothingService
    {
        Task<IEnumerable<ClothingAccountDto>> GetAllUserProductByUserId(string id);
        Task CreateClothing(ClothingCreateDto clothing);
        Task<decimal?> GetClothingPriceById(int clothingId);
        Task ChangePriceById(int id, decimal price);
        Task<IEnumerable<ClothingItemPageDto>> GetAllWithPagination(ClothingPaginationFilterDto paginationFilterDto);
        Task<int> GetCount(int? typeId, int? sizeId);
    }
}
