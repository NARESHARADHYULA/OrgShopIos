using System;
using System.Runtime.Serialization;

namespace TheOrganicShop.Models.Dtos.UserWallet
{
    public class GetUserWalletDtoMobileForView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "userid")]
        public int UserId { get; set; }

        [DataMember(Name = "totalavailableamount")]
        public decimal TotalAvailableAmount { get; set; }

        [DataMember(Name = "totalblockedamount")]
        public decimal TotalBlockedAmount { get; set; }
    }
}
