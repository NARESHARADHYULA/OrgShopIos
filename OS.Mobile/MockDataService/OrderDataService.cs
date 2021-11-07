using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Category;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.OrderDetail;
using TheOrganicShop.Models.Dtos.UserCart;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.MockDataService
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class OrderDataService : IOrderDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private OrderDataService dataService;

        #endregion

        #region Properties

        [DataMember(Name = "ordercalenderinfoitems")]
        public List<GetOrderCalenderInfoDtoMobileForView> OrderCalenderInfo { get; set; }


        public OrderDataService DataService =>
            dataService = PopulateData<OrderDataService>("orgshop.json");

        #endregion

        #region Methods

        
        private static T PopulateData<T>(string fileName)
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            T obj;

            using (Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.MockData.{fileName}"))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }

        public async Task<List<GetOrderSummaryDtoMobileForView>> GetOrderSummaryByUserForMobileAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetOrderCalenderInfoDtoMobileForView>> GetOrderCalenderInfoForMobileAsync(int userId)
        {
            return await Task.FromResult(DataService?.OrderCalenderInfo);
        }

        public async Task<List<GetOrderDetailDtoMobileForView>> GetOrderDetailBySummaryForMobileAsync(int summaryId)
        {
            throw new NotImplementedException();
        }

        public Task<GetOrderSummaryDtoMobileForView> GetOrderSummaryByUserForDateMobileAsync(int userId, DateTime orderedForDateTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOrderSummaryDtoMobileForView>> GetCurrentWeekOrderItemsMobileAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PlaceOrdersAsync(int userAddressId, List<GetUserCartCalenderDtoMobileForView> cartItems)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatusAsync(StatusUpdateDto inputDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderDetailStatusAsync(StatusUpdateDto inputDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<OrderDetail>> GetOrders()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrder(int id)
        {
            throw new NotImplementedException();
        }

        Task<List<GetOrderSummaryDtoMobileForView>> IOrderDataService.GetOrderSummaryByUserForDateMobileAsync(int userId, DateTime orderedForDateTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetOrderDetailDtoMobileForView>> GetOrderDetailByDateAndUserForMobileAsync(int userId, DateTime orderDate)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
