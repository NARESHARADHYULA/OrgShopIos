using System;

namespace TheOrganicShop.Models.Dtos.UserCart
{
    public class CreateOrEditUserCartDtoMobile
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderingForDate { get; set; }

        public int ProductDetailId { get; set; }

        public int Quantity { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? ModifiedByUserId { get; set; }

        public DateTime? ModifiedDateTime { get; set; }
    }
}
