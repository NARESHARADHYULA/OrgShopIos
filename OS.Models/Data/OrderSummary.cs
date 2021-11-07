using System;

namespace TheOrganicShop.Models.Data
{
    public class OrderSummary : AuditedEntity
    {
        public string Number { get; set; }

        public int UserAddressId { get; set; }

        public DateTime PlacedDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string DeliveryTimeSlot { get; set; }

        public int PaymentModeId { get; set; }

        public decimal TotalAmount { get; set; }

        public int OrderStatusId { get; set; }

        public int? CouponId { get; set; }

        public int? UserWalletTransactionHistoryId { get; set; }

    }
}
