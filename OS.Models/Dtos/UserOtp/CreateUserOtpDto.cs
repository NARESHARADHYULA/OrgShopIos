using System;
using System.Collections.Generic;
using System.Text;

namespace TheOrganicShop.Models.Dtos.UserOtp
{
    public class CreateUserOtpDto
    {
        public string ContactNumber { get; set; }

        public string Otp { get; set; }

        public DateTime ValidUntil { get; set; }
    }
}
