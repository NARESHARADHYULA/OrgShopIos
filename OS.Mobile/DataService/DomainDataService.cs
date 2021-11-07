using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.Banner;
using TheOrganicShop.Models.Dtos.DomainData;
using TheOrganicShop.Tools;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.DataService
{
    /// <summary>
    /// Data service to load the data from database using web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DomainDataService : IDomainDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="DomainDataService" /> class.
        /// </summary>
        public DomainDataService()
        {
            httpClient = HttpHelper.GetHttpClient(); ;
        }

        #endregion

        #region Methods

        public async Task<List<GetCalenderDtoMobileForView>> GetCalenderDaysForMobileAsync()
        {
            List<GetCalenderDtoMobileForView> calenderDtoMobileForViews = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}DomainData/GetCalenderDaysForMobileAsync");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var categories = JsonConvert.DeserializeObject<List<GetCalenderDtoMobileForView>>(result);
                        if (categories != null)
                            calenderDtoMobileForViews = categories;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return calenderDtoMobileForViews;
        }

        public async Task<List<GetCitiesDtoMobileForView>> GetCitiesForMobileAsync(bool deliveryEnabledOnly = true)
        {
            List<GetCitiesDtoMobileForView> cities = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}DomainData/GetAlCitiesForMobileAsync?deliveryEnabledOnly={deliveryEnabledOnly}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var citiesDto = JsonConvert.DeserializeObject<List<GetCitiesDtoMobileForView>>(result);
                        if (citiesDto != null)
                            cities = citiesDto;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return cities;
        }

        public async Task<List<GetPinCodesDtoMobileForView>> GetPinCodesForMobileAsync(int cityId, bool deliveryEnabledOnly = true)
        {
            List<GetPinCodesDtoMobileForView> pinCodes = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}DomainData/GetAllPinCodesByCityForMobileAsync?cityId={cityId}&deliveryEnabledOnly={deliveryEnabledOnly}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var pinCodesDto = JsonConvert.DeserializeObject<List<GetPinCodesDtoMobileForView>>(result);
                        if (pinCodesDto != null)
                            pinCodes = pinCodesDto;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pinCodes;
        }

        public async Task<List<GetBannerDtoForMobileView>> GetBannerItemsAsync()
        {
            List<GetBannerDtoForMobileView> bannerItems = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}DomainData/GetBannerItemsForMobileAsync");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var bannersDto = JsonConvert.DeserializeObject<List<GetBannerDtoForMobileView>>(result);
                        if (bannersDto != null)
                            bannerItems = bannersDto;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bannerItems;
        }

        public async Task<List<GetAreasDtoMobileForView>> GetAreasForMobileAsync(int pinCodeId, bool deliveryEnabledOnly = true)
        {
            List<GetAreasDtoMobileForView> areas = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}DomainData/GetAllAreasByPinCodesForMobileAsync?pinCodeId={pinCodeId}&deliveryEnabledOnly={deliveryEnabledOnly}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var areasDto = JsonConvert.DeserializeObject<List<GetAreasDtoMobileForView>>(result);
                        if (areasDto != null)
                            areas = areasDto;
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return areas;
        }

        #endregion
    }
}