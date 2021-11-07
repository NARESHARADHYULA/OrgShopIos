using System;
using System.Collections.Generic;
using System.Text;

namespace TheOrganicShop.Models.Dtos.Payment
{
    public class PaymentResponseDto
    {
        public int UserId { get; set; }

        public string Id { get; set; }
        public string Entity { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AmountDue { get; set; }
        public string Currency { get; set; }
        public string Receipt { get; set; }
        public string OfferId { get; set; }
        public string Status { get; set; }
        public int Attempts { get; set; }
        public string[] Notes { get; set; }
        public string CreatedAt { get; set; }
    }
}
