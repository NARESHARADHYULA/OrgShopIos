using System;

namespace TheOrganicShop.Models.Data
{
    public class OrderTransactionHistory : AuditedEntity
    {
        public int OrderId { get; set; }

        public decimal AmountDeducted { get; set; }

        public DateTime TransactionDate { get; set; }

        public int TransactionTypeId { get; set; }

        public string Comments { get; set; }
    }
}
