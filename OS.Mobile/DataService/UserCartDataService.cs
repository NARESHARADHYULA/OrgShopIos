using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.UserCart;
using TheOrganicShop.Tools;

namespace TheOrganicShop.Mobile.DataService
{
    public class UserCartDataService : IUserCartDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="OrderDataService" /> class.
        /// </summary>
        public UserCartDataService()
        {
            httpClient = HttpHelper.GetHttpClient(); ;
        }

        #endregion
        public async Task<List<GetUserCartCalenderDtoMobileForView>> GetCartItemAsync(int userId)
        {
            List<GetUserCartCalenderDtoMobileForView> cartItems = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}UserCart/GetAllByUserIdForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var results = JsonConvert.DeserializeObject<List<GetUserCartCalenderDtoMobileForView>>(result);
                        if (results != null)
                            cartItems = results;
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

            return cartItems;
        }

        public async Task<int> GetCountByUserIdForMobileAsync(int userId)
        {
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}UserCart/GetCountByUserIdForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        return int.Parse(result);
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

            return 0;
        }


        public async Task<bool> AddCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto)
        {
            // TODO: ISR: Update the return type to object and revisit this method.

            try
            {
                var serializedUser = JsonConvert.SerializeObject(userCartDto);
                var httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}usercart/AddAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> UpdateCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto)
        {
            // TODO: ISR: Update the return type to object and revisit this method.

            try
            {
                var serializedUser = JsonConvert.SerializeObject(userCartDto);
                var httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}usercart/UpdateAsync");
                var response = await httpClient.PutAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> UpdateByIdAsync(int id, int quantity)
        {
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}usercart/UpdateByIdAsync?id={id}&quantity={quantity}");
                var response = await httpClient.PutAsync(uri.ToString(), null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return 1;
                }

                return 0;

            }
            catch (HttpRequestException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public async Task<bool> RemoveCartItemByUserIdAsync(int userId)
        {
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}usercart/DeleteByUserIdAsync?userId={userId}");
                var response = await httpClient.PostAsync(uri.ToString(), null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> RemoveCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto)
        {
            try
            {
                var serializedUser = JsonConvert.SerializeObject(userCartDto);
                var httpContent = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}usercart/DeleteAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (HttpRequestException ex)
            {
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<GetUserCartCalenderDtoMobileForView>> GetOrderedItemsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteByIdAsync(int cartId)
        {
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}usercart/DeleteByIdAsync?id={cartId}");
                var response = await httpClient.PostAsync(uri.ToString(), null);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return 1;
                }

                return 0;

            }
            catch (HttpRequestException ex)
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<bool> IsItemAddedToCartAsync(int userId, int productDetailid)
        {
            throw new NotImplementedException();
        }
    }
}
