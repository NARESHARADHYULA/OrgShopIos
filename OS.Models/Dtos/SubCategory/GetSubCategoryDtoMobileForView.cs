using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.SubCategory
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetSubCategoryDtoMobileForView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

    }
}
