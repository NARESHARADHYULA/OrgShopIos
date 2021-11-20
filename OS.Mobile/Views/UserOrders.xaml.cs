using System;
using System.Linq;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using Syncfusion.ListView.XForms;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.DomainData;
using TheOrganicShop.Models.Dtos.Order;
using Xamarin.Forms.Internals;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("ShowOrdersForDate", "showOrdersForDate")]
    public partial class UserOrders : ContentPage
    {
        private string _showOrdersForDate;
        public string ShowOrdersForDate
        {
            get => _showOrdersForDate;

            set
            {
                _showOrdersForDate = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                listview = calenderview.FindByName("calender") as SfListView;
                orderDataService = DependencyService.Resolve<IOrderDataService>();
                domainDataService = DependencyService.Resolve<IDomainDataService>();
                BindingContext = new OrderViewModel(orderDataService, domainDataService, App.UserId, listview, ShowOrdersForDate);

            }
        }
        public IOrderDataService orderDataService;
        public IDomainDataService domainDataService;
        public SfListView listview;
        public bool renderPageOnOnAppearing = false;
        public UserOrders()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new OrderViewModel(orderDataService, domainDataService, App.UserId, listview, ShowOrdersForDate == null ? "" : ShowOrdersForDate);
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
    }


        [Preserve(AllMembers = true)]
        [DataContract]
        public class OrderViewModel : BaseViewModel
        {
            private bool isLoading = false;
            private bool showNoOrdersPlaced = false;
            public SfListView listView { get; set; }
            public int UserId { get; set; }

            public string ShowOrdersForDate { get; set; }

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
            public bool ShowNoOrdersPlaced
            {
                get { return showNoOrdersPlaced; }
                set
                {
                    this.showNoOrdersPlaced = value;
                    OnPropertyChanged("ShowNoOrdersPlaced");
                }
            }

            public ObservableCollection<GetOrderSummaryDtoMobileForView> OrderSummaryInfoItems
            {
                get => ordersummaryInfoItems;
                set
                {
                    if (ordersummaryInfoItems == value) return;

                    ordersummaryInfoItems = value;
                    OnPropertyChanged("OrderSummaryInfoItems");
                }
            }

            public ObservableCollection<GetOrderSummaryDtoMobileForView> OrderSummaryInfoItemsByDate
            {
                get => ordersummaryInfoItemsByDate;
                set
                {
                    if (ordersummaryInfoItemsByDate == value) return;

                    ordersummaryInfoItemsByDate = value;
                    OnPropertyChanged();
                }
            }

            public ObservableCollection<GetCalenderDtoMobileForView> CalenderObject
            {
                get => calenderObject;
                set
                {
                    calenderObject = value;
                    OnPropertyChanged();
                }
            }

            public GetCalenderDtoMobileForView SelectedCalenderDate
            {
                get => selectedCalenderDate;
                set
                {
                    selectedCalenderDate = value;
                    OnPropertyChanged("SelectedCalenderDate");
                }
            }
            #endregion

            #region Fields

            private ObservableCollection<GetOrderSummaryDtoMobileForView> ordersummaryInfoItems;
            private ObservableCollection<GetOrderSummaryDtoMobileForView> ordersummaryInfoItemsByDate;
            private ObservableCollection<GetCalenderDtoMobileForView> calenderObject;
            private readonly IOrderDataService orderDataService;
            private readonly IDomainDataService domainDataService;
            private DelegateCommand listViewLoadedCommand;
            private DelegateCommand dateSelectedCommand;
            private DelegateCommand orderSelectedCommand;
            private DelegateCommand deleteOrderCommand;
            private GetCalenderDtoMobileForView selectedCalenderDate;
            private DelegateCommand backButtonCommand;
            public DelegateCommand BackButtonCommand =>
              backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

            #endregion

            #region Commands
            /// <summary>
            /// Gets or sets the command that will be executed when the Category is selected.
            /// </summary>
            public DelegateCommand DateSelectedCommand =>
                dateSelectedCommand ?? (dateSelectedCommand = new DelegateCommand(DateSeleceted));

            public DelegateCommand OrderSelectedCommand =>
                orderSelectedCommand ?? (orderSelectedCommand = new DelegateCommand(OrderSelecetedAsync));

            public DelegateCommand ListViewLoadedCommand =>
                listViewLoadedCommand ?? (listViewLoadedCommand = new DelegateCommand(OnListViewLoaded));
           
            
            #endregion


            #region BehaviourCallbacks

            public void OnListViewLoaded(object attachedObject)
            {
                if (SelectedCalenderDate == null)
                {
                    return;
                }

                var selectedItemIndex = listView.DataSource.DisplayItems.IndexOf(listView.SelectedItem);
                selectedItemIndex += (listView.HeaderTemplate != null && !listView.IsStickyHeader || !listView.IsStickyGroupHeader) ? 1 : 0;
                selectedItemIndex -= (listView.GroupHeaderTemplate != null && listView.IsStickyGroupHeader) ? 1 : 0;
                listView.LayoutManager.ScrollToRowIndex(selectedItemIndex, Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
            }

            public void DateSeleceted(object attachedObject)
            {
                if (!(attachedObject is GetCalenderDtoMobileForView) && attachedObject is string)
                {
                    // TODO: ISR: Show respective page here
                    return;
                }

                var selectedDate = attachedObject as GetCalenderDtoMobileForView;
                GetOrdersForSelectedDateAsync(UserId,selectedDate.Date);
            }

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

                var queryParams = new OrderDetailsQueryParam { 
                    OrderSummaryId = selectedOrder.Id, 
                    OrderReferenceNo = selectedOrder.Number, 
                    EnableOrderDelete = enableOrderDelete, 
                    OrderedForDate = selectedOrder.OrderedForDate.ToString("dd/MM/yyyy"),
                    OrderStatus = selectedOrder.OrderStatus,
                    StatusCode = selectedOrder.StatusCode,
                };
                await Shell.Current.GoToAsync($"orderdetails?queryParams={JsonConvert.SerializeObject(queryParams)}");

            }

            #endregion

            #region Constructor

            /// <summary>
            /// Initializes a new instance for the <see cref="OrderCalenderViewModel" /> class.
            /// </summary>
            public OrderViewModel(IOrderDataService orderCalenderInfoDataService, IDomainDataService domainDataService, int userId, SfListView listView, string showOrdersForDate)
            {
                IsLoading = true;
                this.domainDataService = domainDataService;
                this.orderDataService = orderCalenderInfoDataService;
                this.UserId = userId;
                this.listView = listView;
                this.ShowOrdersForDate = showOrdersForDate;

                Device.BeginInvokeOnMainThread(() =>
                {

                    FetchCalenderData();
                    

                });
            }

            #endregion

            public void SelectDate(DateTime date)
            {

                var currentDate = date;
                var currentDateCalenderObject = CalenderObject.Where(item => item.Date.ToString() == currentDate.ToString()).FirstOrDefault();
                // setting the list view selected item
                SelectedCalenderDate = currentDateCalenderObject;
                currentDateCalenderObject.LabelTextColor = Color.FromHex("#FFFFFF");
                // getting the scroll posotion of the list view selected position
                var selectedItemIndex = listView.DataSource.DisplayItems.IndexOf(listView.SelectedItem);
                selectedItemIndex += (listView.HeaderTemplate != null && !listView.IsStickyHeader || !listView.IsStickyGroupHeader) ? 1 : 0;
                selectedItemIndex -= (listView.GroupHeaderTemplate != null && listView.IsStickyGroupHeader) ? 1 : 0;
                listView.LayoutManager.ScrollToRowIndex(selectedItemIndex, Syncfusion.ListView.XForms.ScrollToPosition.Center, true);
                //Progrmatically select the orders for the given date
                GetOrdersForSelectedDateAsync(UserId, date);
                IsLoading = false;
            }

            /// <summary>   
            /// Invoked when an back button is clicked.
            /// </summary>
            /// <param name="obj">The Object</param>
            private async void BackButtonClicked(object obj)
            {
                if (Shell.Current.CurrentState.Location.OriginalString.Contains("orderplaced"))
                { // this is coming from the final page need to clear the navigation stack..
                    Application.Current.MainPage = new AppShell();
                }
                Shell.Current.SendBackButtonPressed();
            }


            public async void FetchCalenderData()
            {
                IsLoading = true;
                try
                {
                    var calenderItems = await domainDataService.GetCalenderDaysForMobileAsync();
                    if (calenderItems != null && calenderItems.Count > 0)
                    {
                        CalenderObject = new ObservableCollection<GetCalenderDtoMobileForView>(calenderItems);
                        // select the date programatically for current date 
                        var date = DateTime.Today;
                        if (ShowOrdersForDate!= string.Empty)
                        {
                            date = Convert.ToDateTime(ShowOrdersForDate);
                        }
                        SelectDate(date);
                    }

                }
                catch (Exception ex)
                {
                    IsLoading = false;
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }

                IsLoading = false;

            }


            public async Task GetOrdersForSelectedDateAsync(int userId, DateTime date)
            {
                IsLoading = true;

                try
                {
                    var order = await orderDataService.GetOrderSummaryByUserForDateMobileAsync(userId, date);
                    var orders = (order == null) ? new List<GetOrderSummaryDtoMobileForView>() :  order;
                    OrderSummaryInfoItems = new ObservableCollection<GetOrderSummaryDtoMobileForView>(orders);
                    ShowNoOrdersPlaced = OrderSummaryInfoItems.Count == 0;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }
                IsLoading = false;
            }

            
        }
    }
}