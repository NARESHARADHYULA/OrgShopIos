using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.Views;
using TheOrganicShop.Models.Dtos.UserAddress;
using TheOrganicShop.Models.Dtos.UserCart;
using TheOrganicShop.Models.Dtos.UserWallet;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CheckOutViewModel : BaseViewModel
    {
        private bool shouldEnablePlaceOrder;
        public bool ShouldEnablePlaceOrder
        {
            get { return shouldEnablePlaceOrder; }
            set
            {
                this.shouldEnablePlaceOrder = value;
                OnPropertyChanged("ShouldEnablePlaceOrder");
            }
        }
        private bool isLoading = false;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        private DelegateCommand backButtonCommand;
        public DelegateCommand BackButtonCommand =>
          backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="UserCartViewModel" /> class.
        /// </summary>
        public CheckOutViewModel(IUserDataService userDataService, IUserCartDataService cartDataService, IOrderDataService orderDataService)
        {
            this.cartDataService = cartDataService;
            this.orderDataService = orderDataService;
            this.userDataService = userDataService;

            Device.InvokeOnMainThreadAsync(async () =>
            {
                isLoading = true;
                try
                {
                    await PopupNavigation.Instance.PushAsync(new LoaderPage());
                    await FetchUserAddress();
                    await FetchUserWallet();
                    await FetchCartAmount();
                    await PopupNavigation.Instance.PopAsync();
                    isLoading = false;
                }
                finally{
                    await PopupNavigation.Instance.PopAsync();
                }
            });
        }

        #endregion

        #region Fields

        private List<GetUserCartCalenderDtoMobileForView> userCarts;

        private GetUserAddressDtoMobileForView userAddress;

        private GetUserWalletDtoMobileForView userWallet;

        private double totalPrice;

        private double discountPrice;

        private double discountPercent;

        private double percent;

        private bool isEmptyViewVisible;

        private Command placeOrderCommand;

        private Command removeCommand;

        private DelegateCommand quantitySelectedCommand;

       

        private readonly IUserCartDataService cartDataService;

        private readonly IOrderDataService orderDataService;

        private readonly IUserDataService userDataService;

        private decimal cartAmount;


        #endregion

        #region Public properties

        public decimal CartAmount
        {
            get => cartAmount;
            set
            {
                cartAmount = value;
                OnPropertyChanged("CartAmount");
            }
        }

        public GetUserAddressDtoMobileForView UserAddress
        {
            get => userAddress;

            set
            {
                if (userAddress == value) return;

                userAddress = value;
                OnPropertyChanged();
            }
        }

        public GetUserWalletDtoMobileForView UserWallet
        {
            get => userWallet;

            set
            {
                if (userWallet == value) return;

                userWallet = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the cart details.
        /// </summary>
        public List<GetUserCartCalenderDtoMobileForView> UserCarts
        {
            get => userCarts;

            set
            {
                if (userCarts == value) return;

                userCarts = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays the total price.
        /// </summary>
        public double TotalPrice
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
                OnPropertyChanged();
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
        public double DiscountPercent
        {
            get => discountPercent;

            set
            {
                if (discountPercent == value) return;

                discountPercent = value;
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

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public Command PlaceOrderCommand =>
            placeOrderCommand ?? (placeOrderCommand = new Command(PlaceOrderClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the Remove button is clicked.
        /// </summary>
        public Command RemoveCommand => removeCommand ?? (removeCommand = new Command(RemoveClicked));

        /// <summary>
        /// Gets or sets the command that will be executed when the quantity is selected.
        /// </summary>
        public DelegateCommand QuantitySelectedCommand =>
            quantitySelectedCommand ?? (quantitySelectedCommand = new DelegateCommand(QuantitySelected));

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
       

        #endregion

        #region Methods

        public async Task FetchUserAddress()
        {
            try
            {

                var result = await userDataService.GetUserAddressAsync(App.UserId);
                if (result != null && result.Count > 0)
                {
                    var results = new ObservableCollection<GetUserAddressDtoMobileForView>(result);
                    UserAddress = results.First();
                }
                else
                {
                    await Shell.Current.GoToAsync("manageaddress");
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task FetchUserWallet()
        {
            try
            {

                var result = await userDataService.GetUserWalletAsync(App.UserId);
                if (result != null)
                {
                    UserWallet = (result);


                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Wallet Found", "OK");
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }


        public async Task FetchCartAmount()
        {
            try
            {

                var result = await cartDataService.GetCartItemAsync(App.UserId);
                if (result != null && UserAddress!= null)
                {
                    CartAmount = result.Sum(x => x.CartItems.Sum(y => y.Quantity * y.DiscountedPrice));
                    UserCarts = (result);
                    if (!UserAddress.IsAddressVerified || UserAddress.IsDeliveryBlocked)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Delivery Address Not Verified", "OK");
                        ShouldEnablePlaceOrder = false;
                    }
                    else if (UserWallet.TotalAvailableAmount <= 0 || UserWallet.TotalAvailableAmount < CartAmount)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "No sufficient balance, please add.", "OK");
                        ShouldEnablePlaceOrder = false;
                        await Shell.Current.GoToAsync("mywallet");
                        return;
                        
                    }
                    else
                    {
                        ShouldEnablePlaceOrder = true;
                    }


                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Cart amount received", "OK");
                }


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
        private async void  PlaceOrderClicked(object obj)
        {
            await PopupNavigation.Instance.PushAsync(new LoaderPage());
            ShouldEnablePlaceOrder = false;
            var addressId = UserAddress.Id;

            var result = await orderDataService.PlaceOrdersAsync(addressId, UserCarts);

            if (result)
            {
                await cartDataService.RemoveCartItemByUserIdAsync(App.UserId);
                await Shell.Current.GoToAsync("orderplaced");
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Order creation failed. Please try again", "OK");
            }
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
                if (selectedItem != null && selectedItem is GetUserCartDtoMobileForView userCart && userCart != null)
                {
                    var status = true; //await cartDataService.UpdateQuantityAsync(1, userCart.ProductDetailId,userCart.Quantity);
                    if (status) UpdatePrice();
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
        public void UpdatePrice()
        {
            //ResetPriceValue();

            //if (UserCarts != null && UserCarts.Count > 0)
            //{
            //    foreach (var cartDetail in UserCarts)
            //    {
            //        if (cartDetail.TotalQuantity == 0) cartDetail.TotalQuantity = 1;

            //        TotalPrice += cartDetail.Product.ActualPrice * cartDetail.TotalQuantity;
            //        DiscountPrice += cartDetail.Product.DiscountPrice * cartDetail.TotalQuantity;
            //        Percent += cartDetail.Product.DiscountPercent;
            //    }

            //    DiscountPercent = percent > 0 ? percent / UserCarts.Count : 0;
            //}
        }

        /// <summary>
        /// This method is used to reset the price amount.
        /// </summary>
        private void ResetPriceValue()
        {
            TotalPrice = 0;
            DiscountPercent = 0;
            DiscountPrice = 0;
            percent = 0;
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
        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }
        #endregion
    }
}
