using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using TheOrganicShop.Models.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckOutPage : ContentPage
    {
        public bool renderPageOnOnAppearing = false;
        IOrderDataService orderDataService;
        IUserCartDataService userCartDataService;
        IUserDataService userDataService;
        public CheckOutPage()
        {
            InitializeComponent();

            orderDataService = DependencyService.Resolve<IOrderDataService>();
            userCartDataService = DependencyService.Resolve<IUserCartDataService>();
            userDataService = DependencyService.Resolve<IUserDataService>();

            BindingContext = new CheckOutViewModel(userDataService, userCartDataService, orderDataService);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new CheckOutViewModel(userDataService, userCartDataService, orderDataService);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }
    }
}