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
    public class UserAddressViewModel : BaseViewModel
    {
        private bool isLoading = false;
        private bool showForm = false;

        private CreateOrEditUserAddressDtoMobile createOrEditUserAddress;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public bool ShowForm
        {
            get { return showForm; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public CreateOrEditUserAddressDtoMobile CreateOrEditUserAddress
        {
            get { return createOrEditUserAddress; }
            set
            {
                this.createOrEditUserAddress = value;
                OnPropertyChanged();
            }
        }


        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="UserAddressViewModel" /> class.
        /// </summary>
        public UserAddressViewModel(IUserDataService userDataService)
        {
            IsLoading = true;
            this.userDataService = userDataService;
            UserAddress = new ObservableCollection<GetUserAddressDtoMobileForView>();
            Device.InvokeOnMainThreadAsync(async () =>
            {

                await FetchUserAddress();

            });
        }

        #endregion

        #region Fields

        private ObservableCollection<GetUserAddressDtoMobileForView> userAddress;

        private DelegateCommand backButtonCommand;
        private DelegateCommand addAddressClickedCommand;
        private DelegateCommand editAddressClickedCommand;
        private DelegateCommand removeAddressClickedCommand;


        private readonly IUserDataService userDataService;


        #endregion

        #region Public properties

        public ObservableCollection<GetUserAddressDtoMobileForView> UserAddress
        {
            get => userAddress;

            set
            {
                if (userAddress == value) return;

                userAddress = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand AddAddressCommand =>
           addAddressClickedCommand ?? (addAddressClickedCommand = new DelegateCommand(OpenNewAddressForm));
        public DelegateCommand EditAddressCommand =>
           editAddressClickedCommand ?? (editAddressClickedCommand = new DelegateCommand(EditAddress));
        public DelegateCommand RemoveAddressCommand =>
           removeAddressClickedCommand ?? (removeAddressClickedCommand = new DelegateCommand(DeleteAddress));
        public DelegateCommand BackButtonCommand =>
       backButtonCommand ?? (backButtonCommand = new DelegateCommand(BackButtonClicked));
        #endregion

        #region Methods

        public async Task FetchUserAddress()
        {
            try
            {

                var result = await userDataService.GetUserAddressAsync(App.UserId);
                if (result != null && result.Count > 0)
                {
                    UserAddress = new ObservableCollection<GetUserAddressDtoMobileForView>(result);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Address Found", "OK");
                    IsLoading = false;
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsLoading = false;
        }


        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>

        private async void OpenNewAddressForm(object obj)
        {
            if (UserAddress.Count > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Cannot create more than one address", "OK");
            }
            else
            {
                await Shell.Current.GoToAsync($"newaddressform?id={0}");
            }

        }
        private async void EditAddress(object obj)
        {
            var address = obj as GetUserAddressDtoMobileForView;
            await Shell.Current.GoToAsync($"newaddressform?id={address.Id}");
        }
        private async void DeleteAddress(object obj)
        {
            await Shell.Current.GoToAsync("newaddressform");
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
                Application.Current.MainPage = new AppShell();
        }
        #endregion
    }
}
