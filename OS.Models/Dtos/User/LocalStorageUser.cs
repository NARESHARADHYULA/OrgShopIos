using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TheOrganicShop.Models.Dtos.User
{
    public class LocalStorageUser
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public bool IsNewUser { get; set; }
    }
}
