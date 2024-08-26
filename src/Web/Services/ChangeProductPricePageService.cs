using Application.Interfaces;
using Web.Interfaces;

namespace Web.Services
{
    public class ChangeProductPricePageService : IChangeProductPricePageService
    {
        private readonly IPermissionService _permissionService;
        private readonly IClothingService _clothingService;

        public ChangeProductPricePageService(IPermissionService permissionService, IClothingService clothingService)
        {
            _permissionService = permissionService;
            _clothingService = clothingService;
        }

        public async Task<decimal?> GetProductPrice(int productId, string userId)
        {
            await _permissionService.IsProductOwner(productId, userId);
            var productPrice = await _clothingService.GetById(productId);
            return productPrice.OldPrice;
        }

        public async Task ChangePrice(int productId, decimal productPrice, string userId)
        {
            await _permissionService.IsProductOwner(productId, userId);
            await _clothingService.ChangePriceById(productId, productPrice);
        }
    }
}
