namespace TheOrganicShop.Models.Data
{
    public class UserWallet : AuditedEntity
    {
        public int UserId { get; set; }

        public decimal TotalAvailableAmount { get; set; }


    }
}
