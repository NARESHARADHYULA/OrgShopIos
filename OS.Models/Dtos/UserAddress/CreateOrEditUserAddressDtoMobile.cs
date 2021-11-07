using System;
using System.Runtime.Serialization;
using TheOrganicShop.Models.Data;

namespace TheOrganicShop.Models.Dtos.UserAddress
{
    public class CreateOrEditUserAddressDtoMobile : AuditedEntity
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string ApartmentName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public int AreaId { get; set; }

        public bool IsDeliveryBlocked { get; set; }

        public bool IsAddressVerified { get; set; }

        public DateTime? AddressVerifiedOnDate { get; set; }

        public string AddressVerifiedBy { get; set; }

    }
}
