using System;
using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.OrderDetail
{

    public class CreateOrEditOrderDetailDtoMobile
    {
        [DataMember(Name="orderSummaryId")]
        public int OrderSummaryId { get; set; }

        [DataMember(Name="productDetailId")]
        public int ProductDetailId { get; set; }

        [DataMember(Name="quantity")]
        public int Quantity { get; set; }

        [DataMember(Name = "appliedWeight")]
        public string AppliedWeight { get; set; }
        
        [DataMember(Name="appliedPrice")]
        public decimal AppliedPrice { get; set; }

        [DataMember(Name = "orderDetailStatusId")]
        public int OrderDetailStatusId { get; set; }

        [DataMember(Name="createdByUserId")]
        public int CreatedByUserId { get; set; }

        [DataMember(Name="createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [DataMember(Name = "modifiedByUserId")]
        public int? ModifiedByUserId { get; set; }

        [DataMember(Name = "modifiedDateTime")]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
