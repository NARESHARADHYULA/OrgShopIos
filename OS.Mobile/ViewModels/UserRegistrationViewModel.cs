using Rg.Plugins.Popup.Services;
using System;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.Views;
using TheOrganicShop.Models.Dtos.User;
using TheOrganicShop.Models.Dtos.UserOtp;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class UserRegistrationViewModel : BaseViewModel
    {
        private bool isLoading = false;

        private string _contactNumber;
        private string _name;


        private string _otpNumber;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public string ContactNumber
        {
            get => _contactNumber;
            set
            {
                this._contactNumber = value;
                OnPropertyChanged("ContactNumber");
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                this._name = value;
                OnPropertyChanged("Name");
            }
        }
        public string OtpNumber
        {
            get => _otpNumber;
            set
            {
                this._otpNumber = value;
                OnPropertyChanged("OtpNumber");
            }
        }

        private DelegateCommand sendOtpCommand;

        private DelegateCommand otpValidateCommand;


        private readonly IUserDataService _userDataService;
        private string mobileNo;
        public string MobileNo
        {
            get => mobileNo;
            set
            {
                this.mobileNo = value;
                OnPropertyChanged("MobileNo");
            }
        }

    #region Command

    public DelegateCommand OtpValidateCommand =>
            otpValidateCommand ?? (otpValidateCommand = new DelegateCommand(ValidateOtpClicked));

        public DelegateCommand SendOtpCommand =>
            sendOtpCommand ?? (sendOtpCommand = new DelegateCommand(SendOtpClicked));

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="UserWalletViewModel" /> class.
        /// </summary>
        public UserRegistrationViewModel(IUserDataService userDataService,string mobileNo,string name)
        {
            this._userDataService = userDataService;
            this.ContactNumber = mobileNo;
            this.Name = name;
            Device.InvokeOnMainThreadAsync(async () =>
            {
                isLoading = true;
                await SendOtp(mobileNo);
                isLoading = false;
            });
        }

        #endregion

        public async Task SendOtp(string mobileNo)
        {
            try
            {
                var result = await _userDataService.CreateAndSendUserOtpAsync(new CreateUserOtpDto { ContactNumber = ContactNumber });

                if (!result)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "OTP Receive failed,  please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        #region Methods

        /// <summary>
        /// Invoked when the add card button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void SendOtpClicked(object obj)
        {
            try
            {
                
                var result = await _userDataService.CreateAndSendUserOtpAsync(new CreateUserOtpDto { ContactNumber = ContactNumber });
                
                if (!result)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "OTP Receive failed,  please try again.", "OK");
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
        private async void ValidateOtpClicked(object obj)
        {
            try
            {
                if (!validate())
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "Please Enter OTP Details", "OK");
                    return;
                }
                await PopupNavigation.Instance.PushAsync(new LoaderPage());
                var result = await _userDataService.ValidateUserOtpAsync(new UserOtpValidateInputDto
                { Otp = OtpNumber, ContactNumber = ContactNumber });
                if (result)
                {

                    var userId = await _userDataService.AddUserAsync(new CreateUserDto
                    { ContactNumber = ContactNumber, Name = "" });

                    if (userId > 0)
                    {
                        var userInfo = new LocalStorageUser
                        {
                            UserId = userId,
                            UserName = Name,
                            IsNewUser = false
                        };

                        if (!App.MockDataService)
                            LocalStorage.Shared.InsertUserDetail(userInfo);

                        App.UserId = userId;
                        App.UseName = Name;
                        Application.Current.MainPage = new AppShell();
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                    {
                        await PopupNavigation.Instance.PopAsync();
                        await Application.Current.MainPage.DisplayAlert("Error", "User creation failed,  please try again.", "OK");
                    }

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "OTP validation failed,  please try again.", "OK");
                    await PopupNavigation.Instance.PopAsync();
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                await PopupNavigation.Instance.PopAsync();
            }
        }
        public bool validate()
        {
            if (string.IsNullOrEmpty(OtpNumber))
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
