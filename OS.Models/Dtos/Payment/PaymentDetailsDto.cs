using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TheOrganicShop.Models.Dtos.Payment
{
    public class PaymentDetailsDto
    {
        public string OrderId { get; set; }

        public string PaymentId { get; set; }

        public string PaymentSignature { get; set; }

        public string PaymentStatus { get; set; }
    }
}
