using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos;
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
    public class OrderDataService : IOrderDataService
    {
        #region fields

        private readonly HttpClient httpClient;

        #endregion

        #region Constructor

        /// <summary>
        ///     Creates an instance for the <see cref="OrderDataService" /> class.
        /// </summary>
        public OrderDataService()
        {
            httpClient = HttpHelper.GetHttpClient();
        }

        #endregion

        #region Methods

        public async Task<List<GetOrderDetailDtoMobileForView>> GetOrderDetailBySummaryForMobileAsync(int summaryId)
        {
            List<GetOrderDetailDtoMobileForView> orderDetailsDto = null;
            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}OrderDetail/GetOrderDetailBySummaryForMobileAsync?orderSummaryId={summaryId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var orderdetails = JsonConvert.DeserializeObject<List<GetOrderDetailDtoMobileForView>>(result);
                        if (orderdetails != null)
                            orderDetailsDto = orderdetails;
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

            return orderDetailsDto;
        }

        public async Task<List<GetOrderSummaryDtoMobileForView>> GetOrderSummaryByUserForMobileAsync(int userId)
        {
            List<GetOrderSummaryDtoMobileForView> orderSummaryInfo = null;
            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}OrderSummary/GetOrderSummaryByUserForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var orders = JsonConvert.DeserializeObject<List<GetOrderSummaryDtoMobileForView>>(result);
                        if (orders != null)
                            orderSummaryInfo = orders;
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

            return orderSummaryInfo;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var uri = new UriBuilder(
                    $"{App.BaseUri}OrderSummary/DeleteAsync?id={id}");
            try
            {
                var response = await httpClient.GetAsync(uri.ToString());
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

        public async Task<bool> UpdateOrderStatusAsync(StatusUpdateDto inputDto)
        {
            try
            {
                var serializedOrders = JsonConvert.SerializeObject(inputDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}OrderSummary/UpdateOrderStatusAsync");
                var response = await httpClient.PutAsync(uri.ToString(), httpContent);
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

        public async Task<bool> UpdateOrderDetailStatusAsync(StatusUpdateDto inputDto)
        {
            try
            {
                var serializedOrders = JsonConvert.SerializeObject(inputDto);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}OrderDetail/UpdateOrderDetailStatusAsync");
                var response = await httpClient.PutAsync(uri.ToString(), httpContent);
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

        public async Task<List<GetOrderDetailDtoMobileForView>> GetOrderDetailByDateAndUserForMobileAsync(int userId, DateTime orderedForDateTime)
        {
            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}OrderDetail/GetOrderDetailByDateAndUserForMobileAsync?userId={userId}&orderedForDate={orderedForDateTime.AsFormattedString()}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var orders = JsonConvert.DeserializeObject<List<GetOrderDetailDtoMobileForView>>(result);
                        return orders ?? new List<GetOrderDetailDtoMobileForView>();
                    }
                }
                return null;
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

        public async Task<List<GetOrderSummaryDtoMobileForView>> GetOrderSummaryByUserForDateMobileAsync(int userId, DateTime orderedForDateTime)
        {
            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}OrderSummary/GetOrderSummaryByUserForDateMobileAsync?userId={userId}&orderedForDateTime={orderedForDateTime.AsFormattedString()}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var orders = JsonConvert.DeserializeObject<List<GetOrderSummaryDtoMobileForView>>(result);
                        return orders ?? new List<GetOrderSummaryDtoMobileForView>();
                    }
                }
                return null;
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


        public async Task<List<GetOrderCalenderInfoDtoMobileForView>> GetOrderCalenderInfoForMobileAsync(int userId)
        {
            List<GetOrderCalenderInfoDtoMobileForView> orderCalenderInfo = null;
            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}OrderSummary/GetOrderCalenderInfoByUserForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var orders = JsonConvert.DeserializeObject<List<GetOrderCalenderInfoDtoMobileForView>>(result);
                        if (orders != null)
                            orderCalenderInfo = orders;
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

            return orderCalenderInfo;
        }

        public async Task<bool> PlaceOrdersAsync(int userAddressId, List<GetUserCartCalenderDtoMobileForView> cartItems)
        {
            // TODO: ISR: Update the return type to object and revisit this method.

            try
            {
                var createOrEditOrderSummaries = ConvertToCreateOrEditOrderSummaries(userAddressId, cartItems);
                var serializedOrders = JsonConvert.SerializeObject(createOrEditOrderSummaries);
                var httpContent = new StringContent(serializedOrders, Encoding.UTF8, "application/json");
                var uri = new UriBuilder($"{App.BaseUri}OrderSummary/AddAsync");
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

        private List<CreateOrEditOrderSummaryDtoMobile> ConvertToCreateOrEditOrderSummaries(int userAddressId,
            List<GetUserCartCalenderDtoMobileForView> createOrEditOrderSummaries)
        {
            var orderSummaries = new List<CreateOrEditOrderSummaryDtoMobile>();
            foreach (var cartItem in createOrEditOrderSummaries)
            {
                var item = new CreateOrEditOrderSummaryDtoMobile();
                item.UserAddressId = userAddressId;
                item.UserId = App.UserId;
                item.OrderedForDate = DateTime.ParseExact(cartItem.Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                item.CreatedByUserId = App.UserId;
                item.CreatedDateTime = DateTime.Now;
                item.DeliveryDate = item.OrderedForDate;
                item.PlacedDate = DateTime.Now;
                item.Number = "";
                item.DeliveryTimeSlot = "06:00 AM - 07:00 PM";
                item.CreateOrEditOrderDetail = new List<CreateOrEditOrderDetailDtoMobile>();
                item.OrderStatusId = 1;
                item.PaymentModeId = 1;

                foreach (var detailItem in cartItem.CartItems)
                    item.CreateOrEditOrderDetail.Add(new CreateOrEditOrderDetailDtoMobile
                    {
                        ProductDetailId = detailItem.ProductDetailId,
                        AppliedPrice = detailItem.DiscountedPrice,
                        AppliedWeight = detailItem.Weight,
                        Quantity = detailItem.Quantity,
                        OrderDetailStatusId = 1,
                        CreatedByUserId = App.UserId,
                        CreatedDateTime = DateTime.Now
                    });

                item.TotalAmount = item.CreateOrEditOrderDetail.Sum(x => x.Quantity * x.AppliedPrice);
                orderSummaries.Add(item);
            }

            return orderSummaries;
        }

        public async Task<List<GetOrderSummaryDtoMobileForView>> GetCurrentWeekOrderItemsMobileAsync(int userId)
        {
            List<GetOrderSummaryDtoMobileForView> orderSummaries = null;
            try
            {
                var uri = new UriBuilder(
                    $"{App.BaseUri}OrderSummary/GetCurrentWeekOrderSummaryByUserForMobileAsync?userId={userId}");
                var response = await httpClient.GetAsync(uri.ToString());
                if (response != null && response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (result != null)
                    {
                        var orders = JsonConvert.DeserializeObject<List<GetOrderSummaryDtoMobileForView>>(result);
                        if (orders != null)
                            orderSummaries = orders;
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

            return orderSummaries;
        }

       
        #endregion
    }
}