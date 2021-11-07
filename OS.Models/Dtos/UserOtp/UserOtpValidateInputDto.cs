using System;
using System.Collections.Generic;
using System.Text;

namespace TheOrganicShop.Models.Dtos.UserOtp
{
    public class UserOtpValidateInputDto
    {
        public string ContactNumber { get; set; }

        public string Otp { get; set; }

        public DateTime CurrentDateTime { get; set; }

    }
}
