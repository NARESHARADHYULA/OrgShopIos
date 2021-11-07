using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.UserWallet;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class UserWalletTransactionsViewModel : BaseViewModel
    {
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
          backButtonCommand ?? (backButtonCommand = new DelegateCommand(onBackButtonClicked));

        private List<GetUserWalletTransactionHistoryDtoForMobileView> userWalletTransactions;

        private readonly IUserDataService _userDataService;

        public List<GetUserWalletTransactionHistoryDtoForMobileView> UserWalletTransactions
        {
            get => userWalletTransactions;

            set
            {
                if (userWalletTransactions == value) return;

                userWalletTransactions = value;
                OnPropertyChanged();
            }
        }


        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="UserWalletViewModel" /> class.
        /// </summary>
        public UserWalletTransactionsViewModel(IUserDataService userDataService)
        {
            this._userDataService = userDataService;
            Device.InvokeOnMainThreadAsync(async () =>
            {


                await FetchUserWalletTransactions();


            });
        }

        #endregion


        #region Methods

        public async Task FetchUserWalletTransactions()
        {
            IsLoading = true;
            try
            {

                var result = await _userDataService.GetWalletTxHistoriesAsync(App.UserId);
                if (result != null)
                {
                    UserWalletTransactions = (result);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Wallet Transactions Found", "OK");
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsLoading = false;
        }


        private async void onBackButtonClicked(object attachedObject)
        {

            Application.Current.MainPage = new AppShell();

        }

        #endregion
    }
}
