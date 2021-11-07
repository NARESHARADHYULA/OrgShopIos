using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos.Category;
using TheOrganicShop.Tools;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.DataService
{
    /// <summary>
    /// Data service to load the data from database using web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class CategoryDataService : ICategoryDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="CategoryDataService" /> class.
        /// </summary>
        public CategoryDataService()
        {
            httpClient = HttpHelper.GetHttpClient();
        }

        #endregion

        #region Methods

        /// <summary>
        /// To get product categories.
        /// </summary>
        public async Task<List<GetCategoryDtoMobileForView>> GetCategoriesAsync()
        {
            List<GetCategoryDtoMobileForView> Categories = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}Category/GetAllForMobileAsync");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var categories = JsonConvert.DeserializeObject<List<GetCategoryDtoMobileForView>>(result);
                        if (categories != null)
                            Categories = categories;
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

            return Categories;
        }



        #endregion
    }
}