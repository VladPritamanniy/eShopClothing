using System.Security.Claims;
using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IProductPageService
    {
        Task CreateProduct(ClothingCreateViewModel product, ClaimsPrincipal claims);
        Task<IEnumerable<SizeViewModel>> GetAllSizesClothing();
        Task<IEnumerable<TypeViewModel>> GetAllTypesClothing();
    }
}
