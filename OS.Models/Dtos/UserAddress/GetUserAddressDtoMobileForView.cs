using System;
using System.Runtime.Serialization;

namespace TheOrganicShop.Models.Dtos.UserAddress
{
    public class GetUserAddressDtoMobileForView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "userid")]
        public int UserId { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "apartmentname")]
        public string ApartmentName { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        public int CityId { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "pincodeid")]
        public int PinCodeId { get; set; }

        [DataMember(Name = "pincode")]
        public string PinCode { get; set; }

        public int AreaId { get; set; }

        [DataMember(Name = "area")]
        public string Area { get; set; }

        [DataMember(Name = "isdeliveryblocked")]
        public bool IsDeliveryBlocked { get; set; }

        [DataMember(Name = "isaddressverified")]
        public bool IsAddressVerified { get; set; }


    }
}
