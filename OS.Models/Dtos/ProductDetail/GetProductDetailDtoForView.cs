using System;

namespace TheOrganicShop.Models.Dtos.ProductDetail
{
    public class GetProductDetailDtoForView
    {
        public int ProductId { get; set; }

        public string Weight { get; set; }

        public int TotalQuantity { get; set; }

        public int AvailableQuantity { get; set; }

        public decimal ActualPrice { get; set; }

        public decimal? DiscountPercent { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }
    }
}
