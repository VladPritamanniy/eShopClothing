using Web.ViewModels;

namespace Web.Interfaces
{
    public interface IProductPageService
    {
        Task CreateProduct(CreateClothingViewModel product);
        Task<IEnumerable<SizeViewModel>> GetAllSizesClothing();
        Task<IEnumerable<TypeViewModel>> GetAllTypesClothing();
    }
}
