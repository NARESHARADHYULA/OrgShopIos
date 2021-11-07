using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.Category;
using TheOrganicShop.Models.Dtos.ProductDetail;
using TheOrganicShop.Tools;

namespace TheOrganicShop.Mobile.DataService
{
    public class ProductDetailDataService : IProductDetailDataService
    {
        #region fields

        System.Net.Http.HttpClient client;

        public ObservableCollection<GetProductDetailDtoMobileForView> ProductDetails
        {
            get; private set;
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="OrderDataService" /> class.
        /// </summary>
        public ProductDetailDataService()
        {
            client = HttpHelper.GetHttpClient();
        }

        #endregion

        public async Task<ObservableCollection<GetProductDetailDtoMobileForView>> GetProductsForMobileByCategoryAsync(
            int categoryId)
        {

            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}ProductDetail/GetAllByCategoryIdForMobileAsync?categoryId={categoryId}");
                var response = await client.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var results = JsonConvert.DeserializeObject<ObservableCollection<GetProductDetailDtoMobileForView>>(result);
                        if (results != null)
                            ProductDetails = results;
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

            return ProductDetails;

        }

        public async Task<ObservableCollection<GetProductDetailDtoMobileForView>> GetProductsForMobileBySubCategoryAsync(
            int subCategoryId)
        {

            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}ProductDetail/GetAllBySubCategoryIdForMobileAsync?subCategoryId={subCategoryId}");
                var response = await client.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    if (result != null)
                    {
                        var results = JsonConvert.DeserializeObject<ObservableCollection<GetProductDetailDtoMobileForView>>(result);
                        if (results != null)
                            ProductDetails = results;
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

            return ProductDetails;

        }
    }
}
