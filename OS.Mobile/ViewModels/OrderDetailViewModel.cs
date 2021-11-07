using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.OrderDetail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class OrderDetailViewModel : BaseViewModel
    {
        private bool isLoading = false;

        public OrderDetailsQueryParam _queryParam;
        private string orderRefrenceId { get; set; }
        private string orderedForDate { get; set; }
        private string orderStatus { get; set; }
        private bool enableOrderDelete { get; set; }
        public string OrderRefrenceId
        {
            get
            {
                return orderRefrenceId;
            }
            set
            {
                orderRefrenceId = value;
            }
        }
        public bool EnableOrderDelete
        {
            get
            {
                return enableOrderDelete;
            }
            set
            {
                enableOrderDelete = value;
                OnPropertyChanged("EnableOrderDelete");
            }
        }
        public string OrderedForDate
        {
            get
            {
                return orderedForDate;
            }
            set
            {
                orderedForDate = value;
                OnPropertyChanged("OrderedForDate");
            }
        }
        public string OrderStatus
        {
            get
            {
                return orderStatus;
            }
            set
            {
                orderStatus = value;
                OnPropertyChanged("OrderStatus");
            }
        }
        #region Public properties

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public ObservableCollection<GetOrderDetailDtoMobileForView> OrderDetailItems
        {
            get => orderDetailItems;
            set
            {
                if (orderDetailItems == value) return;

                orderDetailItems = value;
                OnPropertyChanged();
            }
        }
        private DelegateCommand deleteOrderCommand;
        public DelegateCommand DeleteOrderCommand =>
               deleteOrderCommand ?? (deleteOrderCommand = new DelegateCommand(UpdateOrderStatusAsync));
        private DelegateCommand deleteOrderDetailsCommand;
        public DelegateCommand DeleteOrderDetailsCommand =>
               deleteOrderDetailsCommand ?? (deleteOrderDetailsCommand = new DelegateCommand(UpdateOrderDetailStatusAsync));

        #endregion

        #region Fields

        private ObservableCollection<GetOrderDetailDtoMobileForView> orderDetailItems;
        private readonly IOrderDataService orderDataService;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OrderCalenderViewModel" /> class.
        /// </summary>
        public OrderDetailViewModel(IOrderDataService orderService,  OrderDetailsQueryParam queryParamsDto)
        {
            IsLoading = true;
            this.orderDataService = orderService;
            _queryParam = queryParamsDto;
            OrderRefrenceId = _queryParam.OrderReferenceNo;
            EnableOrderDelete = _queryParam.EnableOrderDelete;
            OrderedForDate = _queryParam.OrderedForDate;
            OrderStatus = _queryParam.OrderStatus;
            Device.BeginInvokeOnMainThread(() => { FetchOrderDetailItems(_queryParam.OrderSummaryId); });
        }

        #endregion

        public async void FetchOrderDetailItems(int orderSummaryId)
        {
            try
            {
                var orderDetails = await orderDataService.GetOrderDetailBySummaryForMobileAsync(orderSummaryId);
                orderDetails = UpdateDeleteOrderStatuses(orderDetails);
                if (orderDetails != null && orderDetails.Count > 0)
                {
                    OrderDetailItems = new ObservableCollection<GetOrderDetailDtoMobileForView>(orderDetails);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsLoading = false;
        }

        public List<GetOrderDetailDtoMobileForView> UpdateDeleteOrderStatuses(List<GetOrderDetailDtoMobileForView> orderDetails)
        {
            foreach(var order in orderDetails)
            {
                bool enableDelete = false;
                if (EnableOrderDelete)
                {
                    enableDelete = order.DetailStatus == "Placed";
                }
                order.EnableDelete = enableDelete;
            }
            return orderDetails;
        }

        public async void UpdateOrderStatusAsync(Object obj)
        {
            IsLoading = true;

            try
            {
                var ans = await Application.Current.MainPage.DisplayAlert("You are about to delete order for date", _queryParam.OrderedForDate, "Yes", "No");
                if (!ans)
                {
                    IsLoading = false;
                    return;
                }
                var orderStatusUpdate = new StatusUpdateDto
                {
                    ModifiedByUserId = App.UserId,
                    ModifiedDateTime = DateTime.Now,
                    Id = _queryParam.OrderSummaryId,
                    StatusCode = "Cancelled"
                };

                var result = await orderDataService.UpdateOrderStatusAsync(orderStatusUpdate);
                if (result)
                {
                    OrderStatus = "Cancelled";
                    EnableOrderDelete = false;
                    FetchOrderDetailItems(_queryParam.OrderSummaryId);
                }
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                IsLoading = false;
            }
           
        }

        public async void UpdateOrderDetailStatusAsync(Object obj)
        {
            IsLoading = true;

            try
            {
                if (obj != null && obj is GetOrderDetailDtoMobileForView orderDetail && orderDetail != null)
                {
                    var ans = await Application.Current.MainPage.DisplayAlert("You are about to delete order item", orderDetail.ProductName, "Yes", "No");
                    if (!ans)
                    {
                        IsLoading = false;
                        return;
                    }
                    var orderStatusUpdate = new StatusUpdateDto
                    {
                        ModifiedByUserId = App.UserId,
                        ModifiedDateTime = DateTime.Now,
                        Id = orderDetail.Id,
                        StatusCode = "Removed"
                    };

                    var result = await orderDataService.UpdateOrderDetailStatusAsync(orderStatusUpdate);
                    if (result)
                    {
                        EnableOrderDelete = false;
                        FetchOrderDetailItems(_queryParam.OrderSummaryId);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                IsLoading = false;
            }
            
        }
    }
}
