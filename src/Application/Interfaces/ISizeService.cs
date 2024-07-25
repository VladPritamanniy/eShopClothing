using Application.DTO;

namespace Application.Interfaces
{
    public interface ISizeService
    {
        Task<IEnumerable<SizeDto>> GetAllSizesClothing();
    }
}
