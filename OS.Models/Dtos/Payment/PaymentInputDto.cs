using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TheOrganicShop.Models.Dtos.Payment
{
    public class PaymentInputDto
    {
        public string orderId { get; set; }

        public string userId { get; set; }

        public decimal amount { get; set; }

        public string currency { get; set; }
    }
}
