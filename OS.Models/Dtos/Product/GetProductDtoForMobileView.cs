using System;
using System.Runtime.Serialization;

namespace TheOrganicShop.Models.Dtos.Product
{
    public class GetProductDtoForMobileView
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "icon")]
        public byte?[] Icon { get; set; }

        //public DateTime EffectiveFromDate { get; set; }

        //public DateTime EffectiveToDate { get; set; }

        //public TimeSpan? DailyCutOffTime { get; set; }

        //public string Summary { get; set; }

        //public string Description { get; set; }

        //public int? OverAllRating { get; set; }
    }
}
