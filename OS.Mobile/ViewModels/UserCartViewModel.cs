using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.Views;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.UserCart;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class UserCartViewModel : BaseViewModel
    {
        private readonly IOrderDataService _orderDataService;

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="UserCartViewModel" /> class.
        /// </summary>
        public UserCartViewModel(IUserCartDataService cartDataService)
        {
            isLoading = true;
            this.cartDataService = cartDataService;
        }

        public DelegateCommand DateChangedCommand =>
        dateChangedCommand ?? (dateChangedCommand = new DelegateCommand(DateSeleceted));

        private DelegateCommand dateChangedCommand;

        private DelegateCommand dateSelectedCommand;
        public DelegateCommand DateSelectedCommand =>
           dateSelectedCommand ?? (dateSelectedCommand = new DelegateCommand(DateSeleceted));
        private ObservableCollection<GetOrderCalenderInfoDtoMobileForView> _orderCalenderInfoItems;
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
        #endregion

        #region Fields
        private bool isLoading = false;
        public int maxNoOfItemsForProduct = 9;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public Command ProceedCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("checkout");
        });

        public Command<GetUserCartDtoMobileForView> AddQuantityCommand => new Command<GetUserCartDtoMobileForView>((item) =>
        {
            if (item.Quantity < maxNoOfItemsForProduct)
            {
                var cartItemChangedDto = new CartItemChangedDto
                {
                    CartItemId = item.Id,
                    NewQuantity = (item.Quantity + 1),
                    OldQuantity = (item.Quantity)
                };
                QuantitySelected(cartItemChangedDto);
            }
        });

        public Command<GetUserCartDtoMobileForView> RmQuantityCommand => new Command<GetUserCartDtoMobileForView>((item) =>
        {
            if (item.Quantity > 0)
            {
                var cartItemChangedDto = new CartItemChangedDto
                {
                    CartItemId = item.Id,
                    NewQuantity = (item.Quantity - 1),
                    OldQuantity = (item.Quantity)
                };
                QuantitySelected(cartItemChangedDto);
            }



        });
        private ObservableCollection<GetUserCartCalenderDtoMobileForView> userCarts;

        private decimal totalPrice;

        private double discountPrice;

        private int totalItems;

        private double percent;

        private bool isEmptyViewVisible;
        private bool noCartItemsForDay;

        private DelegateCommand placeOrderCommand;

        private Command removeCommand;

        private DelegateCommand removeProductFromCartCommand;
        private DelegateCommand removeEntireDayCartItemsCommand;
        private DelegateCommand quantitySelectedCommand;



        private readonly IUserCartDataService cartDataService;

        private List<object> quantities;

        private DelegateCommand backButtonCommand;
        #endregion

        #region Public properties

        public List<object> Quantities
        {
            get => quantities;
            set
            {
                quantities = value;
                OnPropertyChanged("Quantities");
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public ObservableCollection<GetUserCartCalenderDtoMobileForView> UserCarts
        {
            get => userCarts;

            set
            {
                if (userCarts == value) return;

                userCarts = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<GetUserCartDtoMobileForView> currentDayCartItems;
        public ObservableCollection<GetUserCartDtoMobileForView> CurrentDayCartItems
        {
            get => currentDayCartItems;

            set
            {
                if (currentDayCartItems == value) return;

                currentDayCartItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the total price.
        /// </summary>
        public decimal TotalPrice
        {
            get => totalPrice;

            set
            {
                if (totalPrice == value) return;

                totalPrice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property whether view is visible.
        /// </summary>
        public bool IsEmptyViewVisible
        {
            get => isEmptyViewVisible;

            set
            {
                if (isEmptyViewVisible == value) return;

                isEmptyViewVisible = value;
                OnPropertyChanged("IsEmptyViewVisible");
            }
        }
        public bool NoCartItemsForDay
        {
            get => noCartItemsForDay;

            set
            {
                if (noCartItemsForDay == value) return;

                noCartItemsForDay = value;
                OnPropertyChanged("NoCartItemsForDay");
            }
        }
        private string selectedDate { get; set; }

        public string SelectedDate
        {
            get => selectedDate;

            set
            {
                if (selectedDate == value) return;

                selectedDate = value;
                OnPropertyChanged("SelectedDate");
            }
        }


        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays total discount price.
        /// </summary>
        public double DiscountPrice
        {
            get => discountPrice;

            set
            {
                if (discountPrice == value) return;

                discountPrice = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays discount.
        /// </summary>
        public int TotalItems
        {
            get => totalItems;

            set
            {
                if (totalItems == value) return;

                totalItems = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays percent.
        /// </summary>
        public double Percent
        {
            get => percent;
            set
            {
                if (percent == value) return;
                percent = value;
                OnPropertyChanged();
            }
        }

        private object selectedItem;
        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand PlaceOrderCommand =>
            placeOrderCommand ?? (placeOrderCommand = new DelegateCommand(PlaceOrderClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public Command RemoveCommand => removeCommand ?? (removeCommand = new Command(RemoveClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the quantity is selected.
        /// </summary>
        public DelegateCommand QuantitySelectedCommand =>
            quantitySelectedCommand ?? (quantitySelectedCommand = new DelegateCommand(QuantitySelected));

        public DelegateCommand RemoveProductFromCartCommand =>
            removeProductFromCartCommand ?? (removeProductFromCartCommand = new DelegateCommand(RemoveProductFromCart));
        public DelegateCommand RemoveEntireDayCartItemsCommand =>
            removeEntireDayCartItemsCommand ?? (removeEntireDayCartItemsCommand = new DelegateCommand(RemoveDaysCartItems));



        #endregion
        //public void DateSeleceted(object attachedObject)
        //{
        //    if (!(attachedObject is GetOrderCalenderInfoDtoMobileForView) && attachedObject is string)
        //    {
        //        // TODO: ISR: Show respective page here
        //        return;
        //    }

        //    var selectedOrderCalenderInfoDtoMobileForView = attachedObject as GetOrderCalenderInfoDtoMobileForView;
        //    var selectedDateObj = Convert.ToDateTime(selectedOrderCalenderInfoDtoMobileForView.OrderDate);
        //    var selectedDateObjFormated = DateTime.Parse(selectedDateObj.ToString()).ToString("dd/MM/yyyy");
        //    var orderedCartDate = UserCarts.FirstOrDefault(cartItem => cartItem.FormattedDate == selectedDateObjFormated);
        //    CurrentDayCartItems = (orderedCartDate != null) ? new ObservableCollection<GetUserCartDtoMobileForView>(orderedCartDate.CartItems)
        //                          : new ObservableCollection<GetUserCartDtoMobileForView>(Enumerable.Empty<GetUserCartDtoMobileForView>());
        //    NoCartItemsForDay = CurrentDayCartItems.Count == 0;
        //}

        public void DateSeleceted(object attachedObject)
        {
            if (!(attachedObject is GetUserCartCalenderDtoMobileForView) && attachedObject is string)
            {
                // TODO: ISR: Show respective page here
                return;
            }

            var selectedCartDate = attachedObject as GetUserCartCalenderDtoMobileForView;

            var orderedCartDate = UserCarts.FirstOrDefault(cartItem => cartItem.Date == selectedCartDate.Date);
            CurrentDayCartItems = (orderedCartDate != null) ? new ObservableCollection<GetUserCartDtoMobileForView>(orderedCartDate.CartItems)
                                  : new ObservableCollection<GetUserCartDtoMobileForView>(Enumerable.Empty<GetUserCartDtoMobileForView>());
            NoCartItemsForDay = CurrentDayCartItems.Count == 0;
        }

        public async Task FetchOrderCalenderInfoItems(int userId)
        {

            try
            {
                var items = await _orderDataService.GetOrderCalenderInfoForMobileAsync(userId);
                if (items != null && items.Count > 0)
                {
                    OrderCalenderInfoItems = new ObservableCollection<GetOrderCalenderInfoDtoMobileForView>(items);
                    isEmptyViewVisible = OrderCalenderInfoItems.Count == 0;
                    if (!isEmptyViewVisible) { DateSeleceted(OrderCalenderInfoItems.FirstOrDefault()); }
                    //Todo: naresh move default value setting to seperate method
                    var defaultItem = OrderCalenderInfoItems[0];
                    defaultItem.LabelTextColor = Color.White;
                    SelectedOrderCalenderInfoItem = defaultItem;
                    var currentDate = DateTime.Today;
                    var orderSelectedDate = Convert.ToDateTime(defaultItem.OrderDate);
                    //if (currentDate >= orderSelectedDate)
                    //{
                    //    HelperText = "Cut Off Time Reached";
                    //    ShowAddItem = false;
                    //}
                    //else
                    //{
                    //    if (!SelectedOrderCalenderInfoItem.PlacedAnyOrder)
                    //    {
                    //        HelperText = "No Orders Placed";
                    //    }
                    //    else
                    //    {
                    //        HelperText = "View Orders >";
                    //    }
                    //    ShowAddItem = true;
                    //}
                    //SelectedDate = SelectedOrderCalenderInfoItem.OrderDate;


                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsLoading = false;

        }
        #region Methods

        /// <summary>
        /// This method is for getting the cart items
        /// </summary>
        public async Task FetchCartProducts()
        {

            try
            {

                var cartProducts = await cartDataService.GetCartItemAsync(App.UserId);
                if (cartProducts != null && cartProducts.Count > 0)
                {
                    IsEmptyViewVisible = false;
                    UserCarts = new ObservableCollection<GetUserCartCalenderDtoMobileForView>(cartProducts);
                    DateSeleceted(UserCarts.First());
                    SelectedItem = UserCarts.First();
                    UpdateTotals();
                }
                else
                {

                    IsEmptyViewVisible = true;

                }

                isLoading = false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void PlaceOrderClicked(object obj)
        {
            //  await Application.Current.MainPage.Navigation.PushAsync(new CheckoutPage());
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void RemoveClicked(object obj)
        {
            try
            {
                //if (obj != null && obj is GetUserCartDtoMobileForView userCart && userCart != null)
                //{
                //    var status = await cartDataService.RemoveCartItemAsync(1, userCart.ProductDetailId);
                //    if (status != null && status.IsSuccess)
                //    {
                //        UserCarts.Remove(userCart);
                //        UpdatePrice();

                //        if (userCarts.Count == 0)
                //        {
                //            if (Application.Current.MainPage is NavigationPage &&
                //                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
                //                IsEmptyViewVisible = true;
                //            else
                //                await Application.Current.MainPage.Navigation.PushAsync(new EmptyCartPage(true));
                //        }
                //        else
                //        {
                //            IsEmptyViewVisible = false;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when the quantity is selected.
        /// </summary>
        /// <param name="selectedItem">The Object</param>
        private async void QuantitySelected(object selectedItem)
        {
            try
            {
                if (selectedItem != null && selectedItem is CartItemChangedDto item && item != null)
                {
                    item = selectedItem as CartItemChangedDto;
                    if (item.NewQuantity <= 0)
                    {
                        await cartDataService.DeleteByIdAsync(item.CartItemId);

                        foreach (var cartItemsForDay in UserCarts)
                        {
                            var cartItem = cartItemsForDay.CartItems.FirstOrDefault(d => (d.Id == item.CartItemId));
                            if (cartItem != null)
                            {
                                cartItemsForDay.CartItems.Remove(cartItem);
                                CurrentDayCartItems = cartItemsForDay.CartItems;
                                NoCartItemsForDay = CurrentDayCartItems.Count == 0;
                                if (cartItemsForDay.CartItems.Count == 0)
                                {
                                    UserCarts.Remove(cartItemsForDay);
                                }
                                UpdateTotals();
                                break;
                            }
                        }

                    }
                    else
                    {
                        await cartDataService.UpdateByIdAsync(item.CartItemId, item.NewQuantity);
                        foreach (var cartItemsForDay in UserCarts)
                        {
                            var cartItem = cartItemsForDay.CartItems.FirstOrDefault(d => (d.Id == item.CartItemId));
                            if (cartItem != null)
                            {
                                cartItem.Quantity = item.NewQuantity;
                                UpdateTotals();
                                break;
                            }
                        }
                    }

                    UpdateTotals();
                    if (userCarts.Count == 0)
                    {
                        IsEmptyViewVisible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private async void RemoveDaysCartItems(object obj)
        {

            if (obj != null && obj is GetUserCartCalenderDtoMobileForView userCart && userCart != null)
            {
                var orderingDate = userCart.CartItems[0].OrderingForDate;
                var ans = await Application.Current.MainPage.DisplayAlert("You are about to delete items for date", orderingDate.ToString("dd/MM/yyyy"), "Yes", "No");
                if (!ans)
                {
                    return;
                }

                var dto = new CreateOrEditUserCartDtoMobile
                {
                    Id = 0,
                    ProductDetailId = 0,
                    UserId = App.UserId,
                    OrderingForDate = orderingDate
                };

                var status = await cartDataService.RemoveCartItemAsync(dto);
                if (status)
                {
                    foreach (var cartItemsForDay in UserCarts)
                    {
                        if (cartItemsForDay.Date == userCart.Date)
                        {
                            UserCarts.Remove(cartItemsForDay);
                            if (UserCarts.Count == 0)
                            {
                                IsEmptyViewVisible = true;
                            }
                            return;
                        }
                    }

                }
            }
        }
        private async void RemoveProductFromCart(object obj)
        {
            try
            {
                if (obj != null && obj is GetUserCartDtoMobileForView userCart && userCart != null)
                {
                    var createOrEditUserCartDtoMobile = new CreateOrEditUserCartDtoMobile
                    {
                        UserId = App.UserId,
                        Id = userCart.Id,
                        ProductDetailId = userCart.ProductDetailId,
                        OrderingForDate = userCart.OrderingForDate,
                        Quantity = 0
                    };
                    var status = await cartDataService.RemoveCartItemAsync(createOrEditUserCartDtoMobile);
                    if (status)
                    {
                        foreach (var cartItemsForDay in UserCarts)
                        {
                            var cartItem = cartItemsForDay.CartItems.FirstOrDefault(d => ((d.Id == userCart.Id) && (d.OrderingForDate == userCart.OrderingForDate)));
                            if (cartItem != null)
                            {
                                cartItemsForDay.CartItems.Remove(userCart);
                                CurrentDayCartItems = cartItemsForDay.CartItems;
                                NoCartItemsForDay = CurrentDayCartItems.Count == 0;
                                if (cartItemsForDay.CartItems.Count == 0)
                                {
                                    UserCarts.Remove(cartItemsForDay);

                                }
                                UpdateTotals();
                                break;
                            }
                        }

                        if (userCarts.Count == 0)
                        {
                            IsEmptyViewVisible = true;
                        }

                    }
                    if (userCarts.Count == 0)
                    {
                        IsEmptyViewVisible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// This method is used to update the price amount.
        /// </summary>
        public void UpdateTotals()
        {
            TotalPrice = UserCarts.Sum(x => x.CartItems.Sum(y => y.Quantity * y.DiscountedPrice));

            TotalItems = UserCarts.Sum(x => x.CartItems.Count);

        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked1(object obj)
        {
            if (Application.Current.MainPage is NavigationPage &&
                (Application.Current.MainPage as NavigationPage).CurrentPage is HomePage)
            {
                var mainPage =
                    (((Application.Current.MainPage as NavigationPage).CurrentPage as MasterDetailPage)
                        .Detail as NavigationPage).CurrentPage as TabbedPage;
                mainPage.CurrentPage = mainPage.Children[0];
            }
            else
            {
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>


        #endregion
    }
}
