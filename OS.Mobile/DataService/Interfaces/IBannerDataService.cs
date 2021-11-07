using System.Collections.Generic;
using System.Threading.Tasks;
using TheOrganicShop.Models.Dtos.Banner;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface IBannerDataService
    {
        Task<List<GetBannerDtoForMobileView>> GetBannerItemsForMobileAsync();
    }
}
