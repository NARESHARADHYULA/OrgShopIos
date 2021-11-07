using System;
using TheOrganicShop.Models.Data;

namespace TheOrganicShop.Models.Dtos.Product
{
    public class CreateOrEditProductDto : AuditedEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public byte?[] Icon { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }

        public TimeSpan? DailyCutOffTime { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public int? OverAllRating { get; set; }
    }
}
