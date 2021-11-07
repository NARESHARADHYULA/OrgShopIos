using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ICommand NavigateCommand => new Command(Navigate);
        public ICommand LogOutCommand => new  Command(async() => await LogOutAsync());
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("category", typeof(CategoryPage));
            Routing.RegisterRoute("productdetail", typeof(ProductDetailPage));
            Routing.RegisterRoute("usercart", typeof(UserCartPage));
            Routing.RegisterRoute("checkout", typeof(CheckOutPage));
            Routing.RegisterRoute("newaddressform", typeof(AddressForm));
            Routing.RegisterRoute("manageaddress", typeof(MyAddresses));
            Routing.RegisterRoute("mywallet", typeof(UserWallet));
            Routing.RegisterRoute("wallethistory", typeof(UserWalletTransactionsPage));
            Routing.RegisterRoute("myorders", typeof(UserOrders));
            Routing.RegisterRoute("orderdetails", typeof(OrderDetailsPage));
            Routing.RegisterRoute("home", typeof(HomePage));
            Routing.RegisterRoute("areaverification", typeof(DeliveryAreaVerification));
            Routing.RegisterRoute("otpverification", typeof(UserRegistration));
            Routing.RegisterRoute("orderplaced", typeof(OrderPlaced));
            BindingContext = this;
        }
        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            if (args.Target.Location.OriginalString.Contains("ItemDetailPage"))
            {
                args.Cancel();
            }
        }

        private async void Navigate(object route)
        {
            ShellNavigationState state = Shell.Current.CurrentState;
            await Shell.Current.GoToAsync($"{state.Location}/{route.ToString()}");
            Shell.Current.FlyoutIsPresented = false;
        }

        private async Task LogOutAsync()
        {
            var ans = await Application.Current.MainPage.DisplayAlert("Logout", "Are you sure?", "Yes", "No");
            if (ans == true)
            {
                var userInfo = LocalStorage.Shared.GetUserInfo();
                if (userInfo != null)
                {
                    LocalStorage.Shared.DeleteUserDetail(userInfo);
                }

                Application.Current.MainPage = new DeliveryAreaVerification();
            }
           
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            
        }
    }
}