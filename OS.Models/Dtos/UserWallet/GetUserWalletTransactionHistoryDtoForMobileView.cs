using System;
using System.Runtime.Serialization;

namespace TheOrganicShop.Models.Dtos.UserWallet
{
    public class GetUserWalletTransactionHistoryDtoForMobileView
    {
        [DataMember(Name = "transactiondate")]
        public DateTime TransactionDate { get; set; }

        [DataMember(Name = "amount")] 
        public decimal Amount { get; set; }

        [DataMember(Name = "transactiontype")]
        public string TransactionType { get; set; }

        [DataMember(Name = "transactionstatus")]
        public string TransactionStatus { get; set; }

        [DataMember(Name = "comments")]
        public string Comments { get; set; }

    }
}
