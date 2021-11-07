namespace TheOrganicShop.Models.Data
{
    public class UserAddress : AuditedEntity
    {
        public int UserId { get; set; }
        public string FullAddress { get; set; }
        public int AddressTypeId { get; set; }
    }
}
