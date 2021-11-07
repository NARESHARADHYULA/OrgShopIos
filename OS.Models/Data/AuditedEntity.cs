using System;
using System.ComponentModel.DataAnnotations;

namespace TheOrganicShop.Models.Data
{
    public abstract class AuditedEntity
    {
        [Key]
        public int Id { get; set; }

        public int IsDeleted { get; set; }

        public int CreatedByUserId { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int? ModifiedByUserId { get; set; }

        public DateTime? ModifiedDateTime { get; set; }

    }
}
