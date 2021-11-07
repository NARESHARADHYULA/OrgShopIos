using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.DomainData
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetStatusForMobileView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
