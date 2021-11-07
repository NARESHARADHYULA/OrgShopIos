using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.Extensions;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        IOrderDataService orderDataService;
        IDomainDataService domainDataService;
        IUserCartDataService userCartService;
        public bool renderPageOnOnAppearing = false;
        public  HomePage()
        {
            
            InitializeComponent();
            
            domainDataService = DependencyService.Resolve<IDomainDataService>();
            orderDataService = DependencyService.Resolve<IOrderDataService>();
            userCartService = DependencyService.Resolve<IUserCartDataService>();
            BindingContext = new HomePageViewModel(domainDataService, orderDataService, userCartService);
            
            if(Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                Shell.Current.Navigation.PopAsync();
                Shell.Current.Navigation.PopToRootAsync();
                
            }
            
        }

        private async void Button_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                // Invoke your desired action here
                var item = (Button)sender;
                var currentDate = item.CommandParameter.ToString();
                await Shell.Current.GoToAsync($"category?currentDate={currentDate}");
            }
            catch (Exception ex)
            {
              
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new HomePageViewModel(domainDataService, orderDataService, userCartService);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }

    }
}