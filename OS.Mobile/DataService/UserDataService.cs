using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos.Category;
using TheOrganicShop.Models.Dtos.Payment;
using TheOrganicShop.Models.Dtos.UserAddress;
using TheOrganicShop.Models.Dtos.UserOtp;
using TheOrganicShop.Models.Dtos.UserWallet;
using TheOrganicShop.Tools;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.DataService
{
    /// <summary>
    /// Data service to load the data from database using web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class UserDataService : IUserDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="UserDataService" /> class.
        /// </summary>
        public UserDataService()
        {
            httpClient = HttpHelper.GetHttpClient(); ;
        }

        #endregion

        #region Methods

        public async Task<List<GetUserWalletTransactionHistoryDtoForMobileView>> GetWalletTxHistoriesAsync(int userId)
        {
            List<GetUserWalletTransactionHistoryDtoForMobileView> txHistories = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}UserWallet/GetWalletTxHistoriesByUserIdForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var results = JsonConvert.DeserializeObject<List<GetUserWalletTransactionHistoryDtoForMobileView>>(result);
                        if (results != null)
                            txHistories = results;
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

            return txHistories;
        }

        public async Task<ObservableCollection<GetUserAddressDtoMobileForView>> GetUserAddressAsync(int userId)
        {
            ObservableCollection<GetUserAddressDtoMobileForView> userAddresses = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}UserAddress/GetAllByUserIdForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        userAddresses = JsonConvert.DeserializeObject<ObservableCollection<GetUserAddressDtoMobileForView>>(result);
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

            return userAddresses;
        }

        public async Task<int> AddUserAsync(CreateUserDto userDto)
        {
            try
            {
                var serializedOrders = JsonConvert.SerializeObject(userDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}User/CreateUserAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return int.Parse(result);
                }

                return 0;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddOrUpdateUserAddressAsync(CreateOrEditUserAddressDtoMobile userAddress)
        {
            try
            {
                userAddress.CreatedDateTime = DateTime.Now;
                var serializedOrders = JsonConvert.SerializeObject(userAddress);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}UserAddress/AddOrUpdateAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async void AddMoneyToWalletAsync(CreateOrEditUserWalletTransactionHistoryDto createOrEditUserWalletTransaction)
        {
            GetUserWalletDtoMobileForView userWallet = null;
            try
            {
                var serializedOrders = JsonConvert.SerializeObject(createOrEditUserWalletTransaction);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}UserWallet/AddAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;
                    //if (result != null)
                    //{
                    //    userWallet = JsonConvert.DeserializeObject<GetUserWalletDtoMobileForView>(result);

                    //}
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


        }

        public async Task AddMoneyToWalletByRazorPayAsync(PaymentDetailsDto paymentDetailsDto)
        {

            try
            {
                var serializedOrders = JsonConvert.SerializeObject(paymentDetailsDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}UserWallet/AddByRazorPayAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response == null || !response.IsSuccessStatusCode)
                {
                    //var result = response.Content.ReadAsStringAsync().Result;
                    //if (result != null)
                    //{
                    //    userWallet = JsonConvert.DeserializeObject<GetUserWalletDtoMobileForView>(result);

                    //}
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

        }

        public async Task<bool> AddToUserWaitListAsync(CreateUserWaitListDtoMobile waitListDto)
        {
            try
            {
                var serializedUserOto = JsonConvert.SerializeObject(waitListDto);
                var httpContent = new StringContent(serializedUserOto, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}UserWaitList/AddToUserWaitListAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode) return true;
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

        public async Task<bool> CreateAndSendUserOtpAsync(CreateUserOtpDto createUserOtpDto)
        {
            try
            {
                var serializedUserOto = JsonConvert.SerializeObject(createUserOtpDto);
                var httpContent = new StringContent(serializedUserOto, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}Userotp/CreateAndSendUserOtpAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode) return true;
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

        public async Task<bool> ValidateUserOtpAsync(UserOtpValidateInputDto userOtpValidateInputDto)
        {
            try
            {
                var serializedUserOto = JsonConvert.SerializeObject(userOtpValidateInputDto);
                var httpContent = new StringContent(serializedUserOto, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}Userotp/ValidateUserOtpAsync");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    var validOtp=  await response.Content.ReadAsStringAsync();
                    return bool.Parse(validOtp);
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

        public async Task<GetUserWalletDtoMobileForView> GetUserWalletAsync(int userId)
        {
            GetUserWalletDtoMobileForView userWallet = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}UserWallet/GetByUserIdForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        userWallet = JsonConvert.DeserializeObject<GetUserWalletDtoMobileForView>(result);

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

            return userWallet;
        }


        #endregion


    }
}