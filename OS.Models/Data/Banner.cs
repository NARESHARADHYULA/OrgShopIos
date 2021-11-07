using System;

namespace TheOrganicShop.Models.Data
{
    public class Banner : AuditedEntity
    {
        public string Name { get; set; }

        public byte[] Icon { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }
    }
}
