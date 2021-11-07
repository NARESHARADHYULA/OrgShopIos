using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.Banner
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetBannerDtoForMobileView
    {
        public int Id { get; set; }

        [DataMember(Name = "imageuri")]
        public string ImageUri { get; set; }
    }
}
