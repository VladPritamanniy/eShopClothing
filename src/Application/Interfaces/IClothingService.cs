using Application.DTO;

namespace Application.Interfaces
{
    public interface IClothingService
    {
        Task<IEnumerable<ClothingAccountDto>> GetAllUserProductByUserId(string id);
        Task CreateClothing(ClothingCreateDto clothing);
        Task<int?> GetClothingPriceById(int clothingId);
        Task ChangePriceById(int id, int price);
    }
}
