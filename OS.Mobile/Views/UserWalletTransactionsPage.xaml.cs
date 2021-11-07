using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserWalletTransactionsPage : ContentPage
    {
        public UserWalletTransactionsPage()
        {
            InitializeComponent();
            var userDataService = DependencyService.Resolve<IUserDataService>();
            BindingContext = new UserWalletTransactionsViewModel(userDataService);
        }
    }
}