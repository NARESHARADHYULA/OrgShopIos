using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.Payment;
using TheOrganicShop.Models.Dtos.Payment.Paytm;
using TheOrganicShop.Tools;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.DataService
{
    /// <summary>
    /// Data service to load the data from database using web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class PaymentDataService : IPaymentDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an instance for the <see cref="CategoryDataService" /> class.
        /// </summary>
        public PaymentDataService()
        {
            httpClient = HttpHelper.GetHttpClient(); ;
        }

        #endregion

        #region Methods

        public bool CreatePaytmPaymentOrderAsync(PaytmPaymentResponseDto paymentResponseDto)
        {
           
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}Payment/CreatePaytmPaymentOrderAsync");
                var serializedOrders = JsonConvert.SerializeObject(paymentResponseDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(uri.ToString(), httpContent).Result;
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        /// <summary>
        /// To get product categories.
        /// </summary>
        public async Task<PaymentResponseDto> CreateRazorPayOrderAsync(UserPaymentInputDto paymentInputDto)
        {
            PaymentResponseDto paymentResponseDto = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}Payment/CreateRazorPayOrderAsync");
                var serializedOrders = JsonConvert.SerializeObject(paymentInputDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri.ToString(), httpContent);
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        paymentResponseDto = JsonConvert.DeserializeObject<PaymentResponseDto>(result);
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

            return paymentResponseDto;
        }

       
        public PaytmTransactionResponse InitiateTransactionAsync(PaymentInputDto paymentInputDto)
        {
            PaytmTransactionResponse paymentResponseDto = null;
            try
            {
                var uri = new UriBuilder($"{App.BaseUri}Payment/InitiateTransactionAsync");
                var serializedOrders = JsonConvert.SerializeObject(paymentInputDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(uri.ToString(), httpContent).Result;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        paymentResponseDto = JsonConvert.DeserializeObject<PaytmTransactionResponse>(result);
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

            return paymentResponseDto;
        }

        #endregion
    }
}
