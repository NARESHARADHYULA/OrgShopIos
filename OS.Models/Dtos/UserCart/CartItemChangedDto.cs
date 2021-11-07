using System;
using System.Collections.Generic;
using System.Text;

namespace TheOrganicShop.Models.Dtos.UserCart
{
    public class CartItemChangedDto
    {
        public int CartItemId { get; set; }
        public int OldQuantity { get; set; }
        public int NewQuantity { get; set; }
    }
}
