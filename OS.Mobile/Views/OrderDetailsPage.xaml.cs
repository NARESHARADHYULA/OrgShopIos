using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using TheOrganicShop.Models.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("QueryParams", "queryParams")]
    public partial class OrderDetailsPage : ContentPage
    {
        private string _orderId;
        private string _queryParams;
        private OrderDetailsQueryParam jsonObj;
        public bool renderPageOnOnAppearing = false;
        public IOrderDataService orderDetailDataService;

        public string QueryParams
        {
            get => _queryParams;
            set
            {
                _queryParams = (Uri.UnescapeDataString(value ?? string.Empty));

                OnPropertyChanged();
                orderDetailDataService = DependencyService.Resolve<IOrderDataService>();
                jsonObj = JsonConvert.DeserializeObject<OrderDetailsQueryParam>(_queryParams);
                BindingContext = new OrderDetailViewModel(orderDetailDataService, jsonObj);
            }
        }

        public OrderDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new OrderDetailViewModel(orderDetailDataService, jsonObj);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }
    }
}