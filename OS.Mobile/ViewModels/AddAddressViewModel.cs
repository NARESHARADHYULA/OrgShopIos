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
using TheOrganicShop.Models.Dtos.DomainData;
using TheOrganicShop.Models.Dtos.UserAddress;
using TheOrganicShop.Models.Dtos.UserCart;
using TheOrganicShop.Models.Dtos.UserWallet;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class AddAddressViewModel : BaseViewModel
    {
        private bool isLoading = false;
        public int Id { get; set; }

        private CreateOrEditUserAddressDtoMobile createOrEditUserAddress;

        private ObservableCollection<object> selectionobject = new ObservableCollection<object>();
        public ObservableCollection<object> SelectionObject
        {
            get { return selectionobject; }
            set
            {
                selectionobject = value;
                OnPropertyChanged("SelectionObject");
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
        public AddAddressViewModel(IUserDataService userDataService, IDomainDataService domainDataService, int id)
        {
            IsLoading = true;
            this.userDataService = userDataService;
            this.domainDataService = domainDataService;
            this.Id = id;
            CreateOrEditUserAddress = new CreateOrEditUserAddressDtoMobile
            {
                UserId = App.UserId
            };
            Device.InvokeOnMainThreadAsync(async () =>
            {
            
                if (id != 0)
                {
                    await PopupNavigation.Instance.PushAsync(new LoaderPage());
                    await FetchUserAddress(id, CreateOrEditUserAddress);
                   
                }
                else
                {
                    await FetchCitiesDomainData();
                    IsLoading = false;
                }
               
            });
        }

        #endregion

        #region Fields

        private ObservableCollection<GetUserAddressDtoMobileForView> userAddress;
        private ObservableCollection<GetCitiesDtoMobileForView> cities;
        private ObservableCollection<GetPinCodesDtoMobileForView> pinCodes;
        private ObservableCollection<GetAreasDtoMobileForView> areaNames;
        private GetCitiesDtoMobileForView selectedCity;
        private GetPinCodesDtoMobileForView selectedPinCode;
        private GetAreasDtoMobileForView selectedArea;

        public GetCitiesDtoMobileForView SelectedCity
        {
            get
            {
                return selectedCity;
            }
            set
            {
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
            }
        }
        public GetPinCodesDtoMobileForView SelectedPinCode
        {
            get
            {
                return selectedPinCode;
            }
            set
            {
                selectedPinCode = value;
                OnPropertyChanged("SelectedPinCode");
            }
        }
        public GetAreasDtoMobileForView SelectedArea
        {
            get
            {
                return selectedArea;
            }
            set
            {
                selectedArea = value;
                OnPropertyChanged("SelectedArea");
            }
        }
        private DelegateCommand addAddressClickedCommand;
        private DelegateCommand onCitySelecetedCommand;
        private DelegateCommand onPinCodeSelectedCommand;
        private DelegateCommand onAreaSelectedCommand;


        private readonly IUserDataService userDataService;
        private readonly IDomainDataService domainDataService;


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

        public ObservableCollection<GetCitiesDtoMobileForView> Cities
        {
            get => cities;

            set
            {
                if (cities == value) return;

                cities = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GetPinCodesDtoMobileForView> PinCodes
        {
            get => pinCodes;

            set
            {
                if (pinCodes == value) return;

                pinCodes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<GetAreasDtoMobileForView> AreaNames
        {
            get => areaNames;

            set
            {
                if (areaNames == value) return;

                areaNames = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when the Edit button is clicked.
        /// </summary>
        public DelegateCommand AddAddressCommand =>
           addAddressClickedCommand ?? (addAddressClickedCommand = new DelegateCommand(AddAddressClicked));
        public DelegateCommand OnCitySelecetedCommand =>
          onCitySelecetedCommand ?? (onCitySelecetedCommand = new DelegateCommand(onCitySeleceted));
        public DelegateCommand OnPinCodeSelectedCommand =>
         onPinCodeSelectedCommand ?? (onPinCodeSelectedCommand = new DelegateCommand(onPinCodeSelected));
        public DelegateCommand OnAreaSelectedCommand =>
         onAreaSelectedCommand ?? (onAreaSelectedCommand = new DelegateCommand(onAreaSelected));



        #endregion

        #region Methods

        public async Task FetchUserAddress(int id, CreateOrEditUserAddressDtoMobile createOrEditUserAddress)
        {
            try
            {
                await FetchCitiesDomainData();
                var result = await userDataService.GetUserAddressAsync(App.UserId);
                if (result != null && result.Count > 0)
                {
                    UserAddress = new ObservableCollection<GetUserAddressDtoMobileForView>(result);
                    await PopulateAddressForm(id, createOrEditUserAddress);

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Address Found", "OK");
                }

                await PopupNavigation.Instance.PopAsync();
            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PopAsync();
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task PopulateAddressForm(int id, CreateOrEditUserAddressDtoMobile createOrEditUserAddress)
        {
            try
            {
                var address = UserAddress.Where(a => a.Id == id).FirstOrDefault();
                createOrEditUserAddress.UserName = address.UserName;
                createOrEditUserAddress.AreaId = address.AreaId;
                createOrEditUserAddress.ApartmentName = address.ApartmentName;
                createOrEditUserAddress.Address = address.Address;
                await PopulateDomainData(address);

                CreateOrEditUserAddress = new CreateOrEditUserAddressDtoMobile
                {
                    Id = id,
                    UserId = App.UserId,
                    UserName = address.UserName,
                    AreaId = address.AreaId,
                    ApartmentName = address.ApartmentName,
                    Address = address.Address,
                    Email = address.Email
                };
                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task PopulateDomainData(GetUserAddressDtoMobileForView address)
        {
            await FetchCitiesDomainData();
            await FetchPincodesDomainData(address.CityId);
            await FetchAreaNameAndPopulateFields(address);
        }


        public async Task FetchCitiesDomainData()
        {
            try
            {

                var result = await domainDataService.GetCitiesForMobileAsync();
                if (result != null && result.Count > 0)
                {
                    Cities = new ObservableCollection<GetCitiesDtoMobileForView>(result);
                    

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Address Found", "OK");
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task FetchPincodesDomainData(int cityId)
        {
            try
            {

                var result = await domainDataService.GetPinCodesForMobileAsync(cityId);
                if (result != null && result.Count > 0)
                {
                    PinCodes = new ObservableCollection<GetPinCodesDtoMobileForView>(result);

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Address Found", "OK");
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        public async Task FetchAreaNameAndPopulateFields(GetUserAddressDtoMobileForView address)
        {
            await FetchAreaNamesDomainData(address.PinCodeId);
            SelectedCity = Cities.FirstOrDefault(c => c.Id == address.CityId);
            SelectedPinCode = PinCodes.FirstOrDefault(p => p.Id == address.PinCodeId);
            SelectedArea = AreaNames.FirstOrDefault(a => a.Id == address.AreaId);
        }
        public async Task FetchAreaNamesDomainData(int pinCodeId)
        {
            try
            {

                var result = await domainDataService.GetAreasForMobileAsync(pinCodeId);
                if (result != null && result.Count > 0)
                {
                    AreaNames = new ObservableCollection<GetAreasDtoMobileForView>(result);

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Address Found", "OK");
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
        private async void AddAddressClicked(object obj)
        {
            if (!validate())
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Please Enter Mandatory Fields", "OK");
                return;
            }
            var result = await userDataService.AddOrUpdateUserAddressAsync(CreateOrEditUserAddress);

            if (result)
            {
                //await Shell.Current.GoToAsync("manageaddress");
                await Application.Current.MainPage.DisplayAlert("Info", "We are verifying your address, you cannot place orders untill address is verified.", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Address creation failed. Please try again", "OK");
                //await Shell.Current.GoToAsync("manageaddress");
                await Shell.Current.GoToAsync("..");
            }
        }

        public bool validate()
        {
            if (CreateOrEditUserAddress.AreaId == 0 || string.IsNullOrEmpty(CreateOrEditUserAddress.Address) ||string.IsNullOrEmpty(CreateOrEditUserAddress.ApartmentName)|| string.IsNullOrEmpty(CreateOrEditUserAddress.Email)||
                string.IsNullOrEmpty(CreateOrEditUserAddress.UserName))
            {
                return false;
            }
            return true;
        }

        public void onCitySeleceted(object attachedObject)
        {
            if (!(attachedObject is GetCitiesDtoMobileForView) && attachedObject is string)
            {
                // TODO: ISR: Show respective page here
                return;
            }
            var selecetedCity = attachedObject as GetCitiesDtoMobileForView;
            FetchPincodesDomainData(selecetedCity.Id);

        }

        private void onPinCodeSelected(object attachedObject)
        {
            if (!(attachedObject is GetPinCodesDtoMobileForView) && attachedObject is string)
            {
                // TODO: ISR: Show respective page here
                return;
            }
            var selecetedPincode = attachedObject as GetPinCodesDtoMobileForView;
            FetchAreaNamesDomainData(selecetedPincode.Id);
        }

        private void onAreaSelected(object attachedObject)
        {
            if (!(attachedObject is GetAreasDtoMobileForView) && attachedObject is string)
            {
                // TODO: ISR: Show respective page here
                return;
            }
            var selectedArea = attachedObject as GetAreasDtoMobileForView;
            createOrEditUserAddress.AreaId = selectedArea.Id;
        }

        #endregion
    }
}
