namespace TheOrganicShop.Models.Data
{
    public class ComplaintAttachment : AuditedEntity
    {
        public int ComplaintId { get; set; }

        public byte[] Image { get; set; }

    }
}
