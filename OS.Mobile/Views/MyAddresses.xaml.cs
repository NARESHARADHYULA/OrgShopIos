using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyAddresses : ContentPage
    {
        public IUserDataService userDataService;
        public bool renderPageOnOnAppearing = false;
        public MyAddresses()
        {
            InitializeComponent();
            userDataService = DependencyService.Resolve<IUserDataService>();
            BindingContext = new UserAddressViewModel(userDataService);
            if (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                Shell.Current.Navigation.PopAsync();
                Shell.Current.Navigation.PopToRootAsync();

            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new UserAddressViewModel(userDataService);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }
    }
}