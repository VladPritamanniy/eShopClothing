﻿using Web.ViewModels.Base;

namespace Web.ViewModels
{
    public class BasketItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public PriceViewModel Price { get; set; }
        public string ImagePublicId { get; set; }
        public string SizeName { get; set; }
    }
}
