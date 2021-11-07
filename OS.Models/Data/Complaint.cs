namespace TheOrganicShop.Models.Data
{
    public class Complaint : AuditedEntity
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string ReferenceNumber { get; set; }

        public int ComplaintTypeId { get; set; }

        public string ReferenceId { get; set; }

        public int ComplaintStatusId { get; set; }

        public string Comments { get; set; }
    }
}
