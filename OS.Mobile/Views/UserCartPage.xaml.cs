using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCartPage : ContentPage
    {
        public IUserCartDataService userCartDataService;
        public bool renderPageOnOnAppearing = false;
        public UserCartPage()
        {
            InitializeComponent();
            userCartDataService = DependencyService.Resolve<IUserCartDataService>();
            BindingContext = new UserCartViewModel(userCartDataService);
        }

        private async void CheckedOut(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("checkout");
        }

        private void OnImageNameTapped(object sender, EventArgs e)
        {

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new UserCartViewModel(userCartDataService);
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }

    }
}