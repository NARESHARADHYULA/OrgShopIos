using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.OrderDetail;
using TheOrganicShop.Models.Dtos.UserCart;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface IOrderDataService
    {
        Task<List<GetOrderSummaryDtoMobileForView>> GetOrderSummaryByUserForMobileAsync(int userId);
        Task<List<GetOrderCalenderInfoDtoMobileForView>> GetOrderCalenderInfoForMobileAsync(int userId);
        Task<List<GetOrderDetailDtoMobileForView>> GetOrderDetailBySummaryForMobileAsync(int summaryId);
        Task<List<GetOrderSummaryDtoMobileForView>> GetOrderSummaryByUserForDateMobileAsync(int userId, DateTime orderedForDateTime);
        Task<List<GetOrderSummaryDtoMobileForView>> GetCurrentWeekOrderItemsMobileAsync(int userId);
        Task<bool> PlaceOrdersAsync(int userAddressId, List<GetUserCartCalenderDtoMobileForView> cartItems);
        Task<bool> UpdateOrderStatusAsync(StatusUpdateDto inputDto);
        Task<bool> UpdateOrderDetailStatusAsync(StatusUpdateDto inputDto);
        Task<List<GetOrderDetailDtoMobileForView>> GetOrderDetailByDateAndUserForMobileAsync(int userId, DateTime orderDate);
    }
}
