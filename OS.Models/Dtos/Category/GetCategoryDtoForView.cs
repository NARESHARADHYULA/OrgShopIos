using System;

namespace TheOrganicShop.Models.Dtos.Category
{
    public class GetCategoryDtoForView
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public byte?[] Icon { get; set; }

        public DateTime EffectiveFromDate { get; set; }

        public DateTime EffectiveToDate { get; set; }

        public TimeSpan? DailyCutOffTime { get; set; }

        public string Summary { get; set; }

        public string Description { get; set; }

        public int? OverAllRating { get; set; }
    }
}
