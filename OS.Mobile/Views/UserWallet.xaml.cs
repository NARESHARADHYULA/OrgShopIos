using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserWallet : ContentPage
    {
        public IUserDataService userDataService;
        public IPaymentDataService paymentDataService;
        public bool renderPageOnOnAppearing = false;
        public UserWallet()
        {
            InitializeComponent();
            userDataService = DependencyService.Resolve<IUserDataService>();
            paymentDataService = DependencyService.Resolve<IPaymentDataService>();
            BindingContext = new UserWalletViewModel(userDataService, paymentDataService);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new UserWalletViewModel(userDataService, paymentDataService);
            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }

    }
}
