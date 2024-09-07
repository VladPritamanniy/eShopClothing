using System.Security.Claims;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IProductPageService
    {
        Task CreateProductAndNotifySubscribers(ClothingItemCreateViewModel product, ClaimsPrincipal claims);
        Task<IEnumerable<SizeViewModel>> GetAllSizesClothing();
        Task<IEnumerable<TypeViewModel>> GetAllTypesClothing();
    }
}
