using System;
using System.Runtime.Serialization;

namespace TheOrganicShop.Models.Dtos.UserWallet
{
    public class CreateOrEditUserWalletTransactionHistoryDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "userwalletId")]
        public int UserWalletId { get; set; }

        [DataMember(Name = "transactiontypeid")]
        public int TransactionTypeId { get; set; }

        [DataMember(Name = "transactiondate")]
        public DateTime TransactionDate { get; set; }

        [DataMember(Name = "transactionstatusId")]
        public int TransactionStatusId { get; set; }

        [DataMember(Name = "amount")] 
        public decimal Amount { get; set; }

        [DataMember(Name = "walletamountbefore")]
        public decimal WalletAmountBefore { get; set; }

        [DataMember(Name = "paymentreferenceId")]
        public string PaymentReferenceId { get; set; }

        [DataMember(Name = "paymentsource")]
        public string PaymentSource { get; set; }

        [DataMember(Name = "comments")]
        public string Comments { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? ModifiedByUserId { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
