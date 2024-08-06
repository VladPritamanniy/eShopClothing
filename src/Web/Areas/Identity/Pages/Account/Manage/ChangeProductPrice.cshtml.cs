using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Interfaces;
using Web.ViewModels;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Policy = "UserPolicy")]
    public class ChangeProductPriceModel : PageModel
    {
        private readonly IChangeProductPricePageService _changeProductPricePageService;
        public ChangeProductPriceModel(IChangeProductPricePageService changeProductPricePageService)
        {
            _changeProductPricePageService = changeProductPricePageService;
        }

        [BindProperty]
        public ChangeClothingPriceViewModel Item { get; set; }

        public async Task OnGet(int id)
        {
            //var oldPrice = await _changeProductPricePageService.GetProductPriceById(id);
            Item = new ChangeClothingPriceViewModel()
            {
                OldPrice = 100
            };
        }

        public void OnPost(int id)
        {
            //await _changeProductPricePageService.ChangePrice(Item.ValidPrice, User);
        }
    }
}
