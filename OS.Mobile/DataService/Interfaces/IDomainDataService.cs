using System.Collections.Generic;
using System.Threading.Tasks;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos.Banner;
using TheOrganicShop.Models.Dtos.Category;
using TheOrganicShop.Models.Dtos.DomainData;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface IDomainDataService
    {
        Task<List<GetCalenderDtoMobileForView>> GetCalenderDaysForMobileAsync();
        Task<List<GetCitiesDtoMobileForView>> GetCitiesForMobileAsync(bool deliveryEnabledOnly = true);
        Task<List<GetPinCodesDtoMobileForView>> GetPinCodesForMobileAsync(int cityId, bool deliveryEnabledOnly = true);
        Task<List<GetAreasDtoMobileForView>> GetAreasForMobileAsync(int pinCodeId, bool deliveryEnabledOnly = true);
        Task<List<GetBannerDtoForMobileView>> GetBannerItemsAsync();

    }
}