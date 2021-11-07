using System;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("AddressId", "id")]
    public partial class AddressForm : ContentPage
    {
        private string _addressId;
        public string AddressId
        {
            get => _addressId;

            set
            {
                _addressId = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                var userDataService = DependencyService.Resolve<IUserDataService>();
                var domainDataService = DependencyService.Resolve<IDomainDataService>();
                BindingContext = new AddAddressViewModel(userDataService, domainDataService, int.Parse(AddressId));
            }
        }
        public AddressForm()
        {
            InitializeComponent();
            
        }


    }
}