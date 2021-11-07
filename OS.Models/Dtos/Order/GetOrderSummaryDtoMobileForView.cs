using System;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.Order
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetOrderSummaryDtoMobileForView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "orderedfordate")]
        public DateTime OrderedForDate { get; set; }

        [DataMember(Name = "placeddate")]
        public DateTime PlacedDate { get; set; }

        [DataMember(Name = "totalamount")]
        public decimal TotalAmount { get; set; }

        [DataMember(Name = "orderstatus")]
        public string OrderStatus { get; set; }

        [DataMember(Name = "statuscode")]
        public string StatusCode { get; set; }
    }
}
