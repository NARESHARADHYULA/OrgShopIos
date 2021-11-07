using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.OrderDetail
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetOrderDetailDtoMobileForView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "productid")]
        public int ProductId { get; set; }

        [DataMember(Name = "imageuri")]
        public string ImageUri { get; set; }

        [DataMember(Name = "ordersummaryid")]
        public int OrderSummaryId { get; set; }

        [DataMember(Name = "productdetailid")]
        public int ProductDetailId { get; set; }

        [DataMember(Name = "productname")]
        public string ProductName { get; set; }

        [DataMember(Name = "quantity")]
        public decimal Quantity { get; set; }

        [DataMember(Name = "appliedweight")]
        public string AppliedWeight { get; set; }

        [DataMember(Name = "appliedprice")]
        public decimal AppliedPrice { get; set; }

        [DataMember(Name = "detailstatus")]
        public string DetailStatus { get; set; }
        
        public bool EnableDelete { get; set; }

    }
}
