using Application.DTO;

namespace Application.Interfaces
{
    public interface IClothingService
    {
        Task<IEnumerable<ClothingAccountDto>> GetAllUserProductByUserId(string id);
        Task<ClothingDto> CreateClothing(ClothingCreateDto clothing);
        Task<ClothingDto> GetById(int clothingId);
        Task ChangePriceById(int id, decimal price);
        Task<IEnumerable<ClothingItemPageDto>> GetAllWithPagination(ClothingPaginationFilterDto paginationFilterDto);
        Task<int> GetCountFilteredProducts(int? typeId, int? sizeId);
    }
}
