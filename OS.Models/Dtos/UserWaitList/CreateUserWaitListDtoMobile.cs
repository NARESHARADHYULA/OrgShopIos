using System;
using System.Runtime.Serialization;
using TheOrganicShop.Models.Data;

namespace TheOrganicShop.Models.Dtos.UserAddress
{
    public class CreateUserWaitListDtoMobile
    {
        public string ContactNumber { get; set; }

        public string Name { get; set; }

        public int AreaId { get; set; }

        public DateTime WaitListStartDate { get; set; }

        public DateTime WaitListEndDate { get; set; }
    }
}
