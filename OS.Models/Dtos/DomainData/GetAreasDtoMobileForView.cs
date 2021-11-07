﻿using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.DomainData
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetAreasDtoMobileForView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "deliveryenabled")]
        public bool DeliveryEnabled { get; set; }
    }
}
