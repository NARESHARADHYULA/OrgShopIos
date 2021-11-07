using System;

namespace TheOrganicShop.Models.Data
{
    public class UserWalletTransactionHistory : AuditedEntity
    {
        public int UserWalletId { get; set; }
        public DateTime TransactionTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionAmount { get; set; }

        public string PaymentReferenceId { get; set; }

        public string Comments { get; set; }
    }
}
