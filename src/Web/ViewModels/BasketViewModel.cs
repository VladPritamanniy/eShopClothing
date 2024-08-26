using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class BasketViewModel : BaseViewModel
    {
        public string BuyerId { get; set; }
        public List<BasketItemViewModel> BasketItems { get; set; } = new List<BasketItemViewModel>();
    }
}
