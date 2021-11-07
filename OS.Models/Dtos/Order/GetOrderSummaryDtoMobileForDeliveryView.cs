using System;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.Order
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetOrderSummaryDtoMobileForDeliveryView : GetOrderSummaryDtoMobileForView
    {
        [DataMember(Name = "contactnumber")]
        public string ContactNumber { get; set; }

        [DataMember(Name = "address")]
        public string Address { get; set; }

        [DataMember(Name = "areaname")]
        public string AreaName { get; set; }

        [DataMember(Name = "apartmentname")]
        public string ApartmentName { get; set; }

    }
}
