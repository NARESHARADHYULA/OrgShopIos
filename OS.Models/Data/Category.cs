using System;

namespace TheOrganicShop.Models.Data
{
    public class Category : AuditedEntity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public byte[] Icon { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }

        public DateTime? DailyCutOffTime { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

    }
}
