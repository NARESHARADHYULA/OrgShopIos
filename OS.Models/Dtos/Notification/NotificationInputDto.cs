using System;
using System.Collections.Generic;
using System.Text;

namespace TheOrganicShop.Models.Dtos.Notification
{
    public class NotificationInputDto
    {
        public string To { get; set; }

        public string From { get; set; }

        public string Body { get; set; }
    }
}
