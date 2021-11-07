using AllInOneSDK;
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
using TheOrganicShop.Models.Dtos.Payment;
using TheOrganicShop.Models.Dtos.Payment.Paytm;
using TheOrganicShop.Models.Dtos.UserAddress;
using TheOrganicShop.Models.Dtos.UserWallet;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class UserWalletViewModel : BaseViewModel, PaymentCallback
    {
        private bool isLoading = false;

        private bool disableAddMoneyToWallet = true;

        private decimal _moneyToBeAdded;
        private GetUserAddressDtoMobileForView userAddress;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public bool DisableAddMoneyToWallet
        {
            get { return disableAddMoneyToWallet; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("DisableAddMoneyToWallet");
            }
        }

        public decimal MoneyToBeAdded
        {
            get { return _moneyToBeAdded; }
            set
            {
                this._moneyToBeAdded = value;
                OnPropertyChanged("MoneyToBeAdded");
            }
        }

        private DelegateCommand addMoneyCommand;
        private Command addMoneyToWalletCommand;
        private DelegateCommand openWalletHistoryCommand;
        private DelegateCommand viewOrdersCommand;
        private DelegateCommand backButtonCommand;
        private GetUserWalletDtoMobileForView userWallet;

        private readonly IUserDataService _userDataService;
        private readonly IPaymentDataService _paymentDataService;
        PaymentInputDto _paymentInputDto;
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
        #region Command

        public Command AddMoneyToWalletCommand =>
            addMoneyToWalletCommand ?? (addMoneyToWalletCommand = new Command(AddMoneyToWalletClickedAsync));



        public DelegateCommand AddMoneyCommand =>
           addMoneyCommand ?? (addMoneyCommand = new DelegateCommand(AddMoneyToBoxClicked));

        public DelegateCommand OpenWalletHistoryCommand =>
          openWalletHistoryCommand ?? (openWalletHistoryCommand = new DelegateCommand(OpenWalletHistory));

        public DelegateCommand ViewOrdersCommand =>
           viewOrdersCommand ?? (viewOrdersCommand = new DelegateCommand(NavigateToOrdersPage));

        public DelegateCommand BackButtonCommand =>
          backButtonCommand ?? (backButtonCommand = new DelegateCommand(onBackButtonClicked));

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="UserWalletViewModel" /> class.
        /// </summary>
        public UserWalletViewModel(IUserDataService userDataService, IPaymentDataService paymentDataService)
        {
            _userDataService = userDataService;
            _paymentDataService = paymentDataService;

            Device.InvokeOnMainThreadAsync(async () =>
            {
                isLoading = true;
                await PopupNavigation.Instance.PushAsync(new LoaderPage());
                await FetchUserWallet();
                await FetchUserAddress();

                isLoading = false;
                await PopupNavigation.Instance.PopAsync();
            });
        }

        public async Task FetchUserAddress()
        {
            try
            {

                var result = await _userDataService.GetUserAddressAsync(App.UserId);
                if (result != null && result.Count > 0)
                {
                    var results = new ObservableCollection<GetUserAddressDtoMobileForView>(result);
                    UserAddress = results.First();
                    if (!UserAddress.IsAddressVerified || UserAddress.IsDeliveryBlocked)
                    {
                        await Application.Current.MainPage.DisplayAlert("Address not verified", "Redirecting to home page", "OK");
                        Application.Current.MainPage = new AppShell();
                    }

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Address Found", "OK");
                    Application.Current.MainPage = new AppShell();
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                Application.Current.MainPage = new AppShell();
            }
        }
        #endregion


        #region Methods

        private async void onBackButtonClicked(object attachedObject)
        {

            Application.Current.MainPage = new AppShell();

        }

        public async Task FetchUserWallet()
        {
            try
            {

                var result = await _userDataService.GetUserWalletAsync(App.UserId);
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

        /// <summary>
        /// Invoked when the add card button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void AddMoneyToWalletClickedAsync(object obj)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(new LoaderPage());
                // await Device.InvokeOnMainThreadAsync(async () => await PopupNavigation.Instance.PushAsync(new LoaderPage()));
                DisableAddMoneyToWallet = false;
                if (MoneyToBeAdded == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "Please Add Amount.", "OK");
                    await PopupNavigation.Instance.PopAsync();
                    return;
                }
                //MessagingCenter.Send(new UserPaymentInputDto() { UserId = App.UserId, amount = MoneyToBeAdded * 100, currency = "INR" }, "AddMoney");

                var amount = MoneyToBeAdded;

                _paymentInputDto = new PaymentInputDto
                {
                    amount = amount,
                    currency = "INR",
                    orderId = $"ORDER_{App.UserId}{DateTime.Now.Minute}{DateTime.Now.Millisecond}",
                    userId = App.UserId.ToString()
                };

                var paymentResponse = _paymentDataService.InitiateTransactionAsync(_paymentInputDto);

                await PopupNavigation.Instance.PopAsync();

                AllInOnePlugin.startTransaction(_paymentInputDto.orderId, "hmFtQh85822019774200",
                    paymentResponse.body.txnToken, _paymentInputDto.amount.ToString(), "", true, true, this);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Info", $"{ex.Message} {ex.StackTrace}", "OK");
            }
        }

        private void AddMoneyToBoxClicked(object sender)
        {
            // Invoke your desired action here
            var selectedAmount = decimal.Parse(sender.ToString());
            MoneyToBeAdded = selectedAmount;
        }

        private async void OpenWalletHistory(object sender)
        {
            await Shell.Current.GoToAsync("wallethistory");
        }

        public async void NavigateToOrdersPage(object attachedObject)
        {
            await Shell.Current.GoToAsync($"myorders?showOrdersForDate=");
        }

        // override methoods
        public void success(Dictionary<string, object> dictionary)
        {
            PopupNavigation.Instance.PushAsync(new LoaderPage());
            var responseDto = new PaytmPaymentResponseDto()
            {
                Data = new Dictionary<string, string>()
            };

            foreach (string key in dictionary.Keys)
            {
                responseDto.Data.Add(key, dictionary[key].ToString());
            }

            responseDto.Data.Add("USERID", App.UserId.ToString());
            _paymentDataService.CreatePaytmPaymentOrderAsync(responseDto);
            MoneyToBeAdded = 0;
            AllInOnePlugin.DestroyInstance();
            FetchUserWallet();
            PopupNavigation.Instance.PopAsync();
        }

        public void error(string errorMessage)
        {
            var responseDto = new PaytmPaymentResponseDto()
            {
                Data = new Dictionary<string, string>()
            };

            responseDto.Data.Add("ORDERID", _paymentInputDto.orderId);
            responseDto.Data.Add("TXNAMOUNT", _paymentInputDto.amount.ToString());
            responseDto.Data.Add("CURRENCY", _paymentInputDto.currency);
            responseDto.Data.Add("USERID", App.UserId.ToString());
            responseDto.Data.Add("STATUS", "Error");
            responseDto.Data.Add("ERROR", errorMessage);

            _paymentDataService.CreatePaytmPaymentOrderAsync(responseDto);
            AllInOnePlugin.DestroyInstance();

        }

        #endregion
    }
}
