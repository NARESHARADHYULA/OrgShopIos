using System;

namespace TheOrganicShop.Models.Data
{
    public class UserCart : AuditedEntity
    {
        public DateTime OrderingForDate { get; set; }
     
        public int UserId { get; set; }

        public int ProductDetailId { get; set; }

        public string ProductName { get; set; }

        public decimal Weight { get; set; }

        public int Quantity { get; set; }

        public decimal OriginalPrice { get; set; }

        public decimal DiscountPrice { get; set; }

        public decimal AppliedPrice { get; set; }
    }
}
