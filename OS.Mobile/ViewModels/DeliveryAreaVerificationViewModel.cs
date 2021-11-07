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
    public class DeliveryAreaVerificationViewModel : BaseViewModel
    {
        private bool isLoading = false;
        public int Id { get; set; }

        private CreateUserWaitListDtoMobile createUserWaitList;

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


        public CreateUserWaitListDtoMobile CreateUserWaitList
        {
            get { return createUserWaitList; }
            set
            {
                this.createUserWaitList = value;
                OnPropertyChanged();
            }
        }


        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="DeliveryAreaVerificationViewModel" /> class.
        /// </summary>
        public DeliveryAreaVerificationViewModel(IUserDataService userDataService, IDomainDataService domainDataService)
        {
            this.userDataService = userDataService;
            this.domainDataService = domainDataService;

            createUserWaitList = new CreateUserWaitListDtoMobile();
            Device.InvokeOnMainThreadAsync(async () =>
            {
                isLoading = true;
                await FetchCitiesDomainData();
                isLoading = false;
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

        private DelegateCommand onCitySelecetedCommand;
        private DelegateCommand onPinCodeSelectedCommand;
        private DelegateCommand onAreaSelectedCommand;
        private DelegateCommand submitAddressCommand;


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

        public DelegateCommand OnCitySelectedCommand =>
          onCitySelecetedCommand ?? (onCitySelecetedCommand = new DelegateCommand(onCitySelected));
        public DelegateCommand OnPinCodeSelectedCommand =>
         onPinCodeSelectedCommand ?? (onPinCodeSelectedCommand = new DelegateCommand(onPinCodeSelected));
        public DelegateCommand OnAreaSelectedCommand =>
         onAreaSelectedCommand ?? (onAreaSelectedCommand = new DelegateCommand(onAreaSelected));
        public DelegateCommand SubmitAddressCommand =>
        submitAddressCommand ?? (submitAddressCommand = new DelegateCommand(submitAddress));


        #endregion

        #region Methods

        public async Task FetchCitiesDomainData()
        {
            try
            {
                var result = await domainDataService.GetCitiesForMobileAsync(false);
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

                var result = await domainDataService.GetPinCodesForMobileAsync(cityId, false);
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


        public async Task FetchAreaNamesDomainData(int pinCodeId)
        {
            try
            {
                var result = await domainDataService.GetAreasForMobileAsync(pinCodeId, false);
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
        private async void AddWaitListClicked()
        {
            var result = await userDataService.AddToUserWaitListAsync(CreateUserWaitList);

            if (result)
            {
                Application.Current.MainPage = new NavigationPage(new UserWaiting()); 
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to add user to waiting list", "OK");
            }
        }

        public void onCitySelected(object attachedObject)
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
            selectedArea = attachedObject as GetAreasDtoMobileForView;
            createUserWaitList.AreaId = selectedArea.Id;        
        }

        private async void submitAddress(object attachedObject)
        {
            if (!validate())
            {
                await Application.Current.MainPage.DisplayAlert("Info", "Please Enter Phone Number, Name, City, PinCode, Area Details", "OK");
            }
            else if (!selectedArea.DeliveryEnabled)
            {
                createUserWaitList.WaitListStartDate = DateTime.Now;
                createUserWaitList.WaitListEndDate = DateTime.Now.AddYears(20);
                AddWaitListClicked();
            }
            else
            {
                CreateUserWaitList.ContactNumber = "+91" + CreateUserWaitList.ContactNumber;
                Application.Current.MainPage = new NavigationPage(new UserRegistration(CreateUserWaitList.ContactNumber, CreateUserWaitList.Name));
            }
        }
        public bool validate()
        {
            if(createUserWaitList.AreaId == 0|| string.IsNullOrEmpty(createUserWaitList.ContactNumber) || string.IsNullOrEmpty(createUserWaitList.Name))
            {
                return false;
            }
            return true;
        }


        #endregion
    }
}
