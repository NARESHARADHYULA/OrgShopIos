using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TheOrganicShop.Models.Dtos.Payment
{
    public class UserPaymentInputDto : PaymentInputDto
    {
        public int UserId { get; set; }
    }
}
