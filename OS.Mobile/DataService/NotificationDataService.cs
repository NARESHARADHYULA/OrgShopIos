using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.Notification;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.OrderDetail;
using TheOrganicShop.Models.Dtos.UserCart;
using TheOrganicShop.Tools;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.DataService
{
    /// <summary>
    ///     Data service to load the data from database using web API.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class NotificationDataService : INotificationDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates an instance for the <see cref="NotificationDataService" /> class.
        /// </summary>
        public NotificationDataService()
        {
            httpClient = HttpHelper.GetHttpClient();
        }

        #endregion

        #region Methods


        public async Task<bool> SendNotificationAsync(NotificationInputDto notificationInputDto)
        {

            try
            {
                var serializedOrders = JsonConvert.SerializeObject(notificationInputDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}Notification/SendSmsNotificationAsync");
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

        #endregion
    }
}