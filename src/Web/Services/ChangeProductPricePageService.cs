using Application.Interfaces;
using Web.Interfaces;

namespace Web.Services
{
    public class ChangeProductPricePageService : IChangeProductPricePageService
    {
        private readonly IClothingService _clothingService;

        public ChangeProductPricePageService(IClothingService clothingService)
        {
            _clothingService = clothingService;
        }

        public Task ChangePrice(int price)
        {
            throw new NotImplementedException();
        }
    }
}
