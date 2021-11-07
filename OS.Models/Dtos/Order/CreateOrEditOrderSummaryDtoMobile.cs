using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TheOrganicShop.Models.Dtos.OrderDetail;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.Order
{

    public class CreateOrEditOrderSummaryDtoMobile
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "number")]
        public string Number { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "userAddressId")]
        public int UserAddressId { get; set; }

        [DataMember(Name = "orderedForDate")]
        public DateTime OrderedForDate { get; set; }

        [DataMember(Name = "placedDate")]
        public DateTime PlacedDate { get; set; }

        [DataMember(Name = "deliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [DataMember(Name = "deliveryTimeSlot")]
        public string DeliveryTimeSlot { get; set; }

        [DataMember(Name = "paymentModeId")]
        public int PaymentModeId { get; set; }

        [DataMember(Name = "totalAmount")]
        public decimal TotalAmount { get; set; }

        [DataMember(Name = "orderStatusId")]
        public int OrderStatusId { get; set; }

        [DataMember(Name = "couponId")]
        public int? CouponId { get; set; }

        [DataMember(Name = "userWalletTransactionHistoryId")]
        public int? UserWalletTransactionHistoryId { get; set; }

        [DataMember(Name = "createOrEditOrderDetail")]
        public List<CreateOrEditOrderDetailDtoMobile> CreateOrEditOrderDetail { get; set; }

        [DataMember(Name = "createdByUserId")]
        public int CreatedByUserId { get; set; }

        [DataMember(Name = "createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [DataMember(Name = "modifiedByUserId")]
        public int? ModifiedByUserId { get; set; }

        [DataMember(Name = "modifiedDateTime")]
        public DateTime? ModifiedDateTime { get; set; }
    }
}
