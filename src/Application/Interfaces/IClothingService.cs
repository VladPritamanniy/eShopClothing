using Application.DTO;

namespace Application.Interfaces
{
    public interface IClothingService
    {
        Task<IEnumerable<ClothingDto>> GetAllUserProductByUserIdListAsync(string id);
    }
}
