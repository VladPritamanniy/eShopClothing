using Core.Entities;
using Core.Repositories.Base;

namespace Core.Repositories
{
    public interface IClothingRepository : IRepository<Clothing>
    {
        Task<IEnumerable<Clothing>> GetAllUserProductByUserId(string id);
    }
}
