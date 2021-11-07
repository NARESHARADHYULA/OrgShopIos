using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TheOrganicShop.Models.Dtos
{
    public class StatusUpdateDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "statuscode")]
        public string StatusCode { get; set; }

        [DataMember(Name = "modifiedbyuserId")]
        public int ModifiedByUserId { get; set; }

        [DataMember(Name = "modifieddatetime")]
        public DateTime ModifiedDateTime { get; set; }
    }
}
