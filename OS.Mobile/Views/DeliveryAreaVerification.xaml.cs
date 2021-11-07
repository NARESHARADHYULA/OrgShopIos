using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeliveryAreaVerification : ContentPage
    {
        public DeliveryAreaVerification()
        {
            InitializeComponent();
            var userDataService = DependencyService.Resolve<IUserDataService>();
            var domainDataService = DependencyService.Resolve<IDomainDataService>();
            BindingContext = new DeliveryAreaVerificationViewModel(userDataService, domainDataService);
        }
    }
}