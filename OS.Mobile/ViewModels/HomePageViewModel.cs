using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Banner;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.UserCart;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class HomePageViewModel : BaseViewModel
    {
        #region Public properties

        private bool isLoading = false;
        private bool showWeekOrdersLabel = false;
        private bool isActive = true;
        private bool showAddItem = false;


        private int cartItemsCount = 0;
        private bool isSelected = false;
        private string selectedDate = "";
        private string helperText = "No Item Selected";
        private GetOrderCalenderInfoDtoMobileForView selectedOrderCalenderInfoItem { get; set; }

        private GetOrderSummaryDtoMobileForView selectedOrderSummaryItem { get; set; }

        public GetOrderCalenderInfoDtoMobileForView SelectedOrderCalenderInfoItem
        {
            get { return selectedOrderCalenderInfoItem; }
            set
            {
                this.selectedOrderCalenderInfoItem = value;
                OnPropertyChanged("SelectedOrderCalenderInfoItem");
            }
        }

        public GetOrderSummaryDtoMobileForView SelectedOrderSummaryItem
        {
            get { return selectedOrderSummaryItem; }
            set
            {
                this.selectedOrderSummaryItem = value;
                OnPropertyChanged("SelectedOrderSummaryItem");
            }
        }

        public int CartItemsCount
        {
            get { return cartItemsCount; }
            set
            {
                this.cartItemsCount = value;
                OnPropertyChanged("CartItemsCount");
            }
        }
        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        public bool ShowAddItem
        {
            get { return showAddItem; }
            set
            {
                this.showAddItem = value;
                OnPropertyChanged("ShowAddItem");
            }
        }
        
        public bool ShowWeekOrdersLabel
        {
            get { return showWeekOrdersLabel; }
            set
            {
                this.showWeekOrdersLabel = value;
                OnPropertyChanged("ShowWeekOrdersLabel");
            }
        }
        

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsActive");
            }
        }

        public string SelectedDate
        {
            get { return selectedDate; }
            set
            {
                this.selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }

        public string HelperText
        {
            get { return helperText; }
            set
            {
                this.helperText = value;
                OnPropertyChanged("HelperText");
            }
        }

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (value != isSelected)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }



        public ObservableCollection<GetBannerDtoForMobileView> BannerItemsDto
        {
            get => _bannerItemsDto;
            set
            {
                if (_bannerItemsDto == value) return;

                _bannerItemsDto = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GetOrderCalenderInfoDtoMobileForView> OrderCalenderInfoItems
        {
            get => _orderCalenderInfoItems;
            set
            {
                if (_orderCalenderInfoItems == value) return;

                _orderCalenderInfoItems = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GetOrderSummaryDtoMobileForView> CurrentWeekOrderItems
        {
            get => _currentWeekOrderItems;
            set
            {
                if (_currentWeekOrderItems == value) return;

                _currentWeekOrderItems = value;
                OnPropertyChanged();
            }
        }

        public Command SelectionChangedCommand
        {
            get;
            set;
        }

        #endregion

        #region Fields

        private ObservableCollection<GetOrderCalenderInfoDtoMobileForView> _orderCalenderInfoItems;
        private ObservableCollection<GetOrderSummaryDtoMobileForView> _currentWeekOrderItems;
        
        private readonly IOrderDataService _orderDataService;
        private readonly IUserCartDataService _userCartDataService;
        private ObservableCollection<GetBannerDtoForMobileView> _bannerItemsDto;
        private readonly IDomainDataService _domainDataService;
        private DelegateCommand dateSelectedCommand;
        private DelegateCommand viewOrdersCommand;
        private DelegateCommand orderSelectedCommand;
        public Command CheckoutCommand { get; set; }
        public Command ListViewSelectionChanged { get; set; }

        #endregion

        #region Commands
        /// <summary>
        /// Gets or sets the command that will be executed when the Category is selected.
        /// </summary>
        public DelegateCommand DateSelectedCommand =>
            dateSelectedCommand ?? (dateSelectedCommand = new DelegateCommand(DateSeleceted));

        public DelegateCommand ViewOrdersCommand =>
           viewOrdersCommand ?? (viewOrdersCommand = new DelegateCommand(NavigateToOrdersPage));

        public DelegateCommand OrderSelectedCommand =>
               orderSelectedCommand ?? (orderSelectedCommand = new DelegateCommand(OrderSelecetedAsync));
        #endregion

        //private void OnSelectionChanged(object obj)
        //{
        //    foreach (var selectedItem in (obj as Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs).AddedItems)
        //        (selectedItem as GetOrderCalenderInfoDtoMobileForView).IsSelected = true;

        //    foreach (var removedItem in (obj as Syncfusion.ListView.XForms.ItemSelectionChangedEventArgs).RemovedItems)
        //        (removedItem as GetOrderCalenderInfoDtoMobileForView).IsSelected = false;
        //}

        public async void OrderSelecetedAsync(object attachedObject)
        {
            if (!(attachedObject is GetOrderSummaryDtoMobileForView) && attachedObject is string)
            {
                // TODO: ISR: Show respective page here
                return;
            }

            var selectedOrder = attachedObject as GetOrderSummaryDtoMobileForView;
            var enableOrderDelete = false;
            if (selectedOrder.StatusCode == "Placed")
            {
                enableOrderDelete = true;
            }
            var queryParams = new OrderDetailsQueryParam
            {
                OrderSummaryId = selectedOrder.Id,
                OrderReferenceNo = selectedOrder.Number,
                EnableOrderDelete = enableOrderDelete,
                OrderedForDate = selectedOrder.OrderedForDate.ToString("dd/MM/yyyy"),
                OrderStatus = selectedOrder.OrderStatus,
                StatusCode = selectedOrder.StatusCode,
            };
            await Shell.Current.GoToAsync($"orderdetails?queryParams={JsonConvert.SerializeObject(queryParams)}");

        }

        public void DateSeleceted(object attachedObject)
        {
            if (!(attachedObject is GetOrderCalenderInfoDtoMobileForView) && attachedObject is string)
            {
                // TODO: ISR: Show respective page here
                return;
            }

            //Category_collectionview.SelectedItem = null;
            var selectedDateDto = attachedObject as GetOrderCalenderInfoDtoMobileForView;
            var currentDate = DateTime.Today;
            var orderSelectedDate = Convert.ToDateTime(selectedDateDto.OrderDate);
            if (currentDate >= orderSelectedDate)
            {
                HelperText = "Cutoff Time Reached";
                ShowAddItem = false;
            }
            else
            {
                if (!selectedDateDto.PlacedAnyOrder)
                {
                    HelperText = "No Orders Placed";
                }
                else
                {
                    HelperText = "View Orders >";
                }

                ShowAddItem = true;
            }
            SelectedDate = selectedDateDto.OrderDate;
        }

        public async void NavigateToOrdersPage(object attachedObject)
        {
            if (!SelectedOrderCalenderInfoItem.PlacedAnyOrder)
            {
                return;

            }
            var orderdate = selectedOrderCalenderInfoItem.OrderDate;
            await Shell.Current.GoToAsync($"myorders?showOrdersForDate={orderdate}");
        }

        #region Constructor

        public HomePageViewModel(IDomainDataService domainDataService, IOrderDataService orderDataService, IUserCartDataService userCartDataService)
        {
            this._domainDataService = domainDataService;
            this._orderDataService = orderDataService;
            _userCartDataService = userCartDataService;
            IsLoading = true;
            
            Device.InvokeOnMainThreadAsync(async () =>
            {
               
                await FetchOrderCalenderInfoItems(App.UserId);
                await FetchCurrentWeekOrderItems(App.UserId);
                await FetchBannerItems();
                await FetchCartItemsCountAsync(App.UserId);
            });

            CheckoutCommand = new Command(CheckOut);
            SelectionChangedCommand = new Command<Syncfusion.XForms.TabView.SelectionChangedEventArgs>(TabViewSelectionChanged);
        }

        #endregion
        public async void TabViewSelectionChanged(Syncfusion.XForms.TabView.SelectionChangedEventArgs selectionChangedEventArgs)
        {
            IsLoading = true;
            // obj.
            

            if (selectionChangedEventArgs.Name == "Orders")
            {
                await FetchCurrentWeekOrderItems(App.UserId);
            }
            else
            {
                await FetchOrderCalenderInfoItems(App.UserId);
            }
            //App.Current.MainPage.DisplayAlert("Alert", "You have been selected " + selectionChangedEventArgs.Name + "Tab", "OK");
        }

        private async void CheckOut(object obj)
        {
            
            await Shell.Current.GoToAsync("usercart");
        }

        public async Task FetchCartItemsCountAsync(int userId)
        {
            try
            {
                var dto = new CreateOrEditUserCartDtoMobile
                {
                    Id = 0,
                    ProductDetailId = 0,
                    UserId = App.UserId,
                    OrderingForDate = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture)
                };
                await _userCartDataService.RemoveCartItemAsync(dto);
                CartItemsCount = await _userCartDataService.GetCountByUserIdForMobileAsync(App.UserId);
            }
            catch(Exception ex)
            {

            }
        }


        public async Task FetchOrderCalenderInfoItems(int userId)
        {
            
            try
            {
                var items = await _orderDataService.GetOrderCalenderInfoForMobileAsync(userId);
                if (items != null && items.Count > 0)
                {
                    OrderCalenderInfoItems = new ObservableCollection<GetOrderCalenderInfoDtoMobileForView>(items);
                    //Todo: naresh move default value setting to seperate method
                    var defaultItem = OrderCalenderInfoItems[0];
                    defaultItem.LabelTextColor = Color.White;
                    SelectedOrderCalenderInfoItem = defaultItem;
                    var currentDate = DateTime.Today;
                    var orderSelectedDate = Convert.ToDateTime(defaultItem.OrderDate);
                    if (currentDate >= orderSelectedDate)
                    {
                        HelperText = "Cutoff Time Reached";
                        ShowAddItem = false;
                    }
                    else
                    {
                        if (!SelectedOrderCalenderInfoItem.PlacedAnyOrder)
                        {
                            HelperText = "No Orders Placed";
                        }
                        else
                        {
                            HelperText = "View Orders >";
                        }
                        ShowAddItem = true;
                    }
                    SelectedDate = SelectedOrderCalenderInfoItem.OrderDate;


                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsLoading = false;
            
        }

        public async Task FetchCurrentWeekOrderItems(int userId)
        {

            try
            {
                var items = await _orderDataService.GetCurrentWeekOrderItemsMobileAsync(userId);
                if (items != null && items.Count > 0)
                {
                    CurrentWeekOrderItems = new ObservableCollection<GetOrderSummaryDtoMobileForView>(items);
                    
                }
                else
                {
                    ShowWeekOrdersLabel = true;
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsLoading = false;
           
        }
        public async Task FetchBannerItems()
        {
            try
            {
                var bannerItems = await _domainDataService.GetBannerItemsAsync();
                if (bannerItems != null && bannerItems.Count > 0)
                {
                    BannerItemsDto = new ObservableCollection<GetBannerDtoForMobileView>(bannerItems);
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
