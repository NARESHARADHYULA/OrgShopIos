namespace TheOrganicShop.Models.Data
{
    public class OrderDetail : AuditedEntity
    {

        public int OrderSummaryId { get; set; }

        public int ProductDetailId { get; set; }

        public int Quantity { get; set; }

        public decimal AppliedPrice { get; set; }


    }
}
