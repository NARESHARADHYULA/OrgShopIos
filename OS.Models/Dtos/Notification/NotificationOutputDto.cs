using System;
using System.Collections.Generic;
using System.Text;

namespace TheOrganicShop.Models.Dtos.Notification
{
    public class NotificationOutputDto
    {
      public bool IsSuccess { get; set; }

      public string ErrorMessage { get; set; }
    }
}
