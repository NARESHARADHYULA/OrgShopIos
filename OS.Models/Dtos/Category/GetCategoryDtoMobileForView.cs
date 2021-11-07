using System.Collections.Generic;
using System.Runtime.Serialization;
using TheOrganicShop.Models.Dtos.SubCategory;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.Category
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetCategoryDtoMobileForView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "imageuri")]
        public string ImageUri { get; set; }
    }
}
