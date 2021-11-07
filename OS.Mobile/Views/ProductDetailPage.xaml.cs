using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Syncfusion.XForms.TabView;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.ViewModels;
using TheOrganicShop.Models.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("QueryParams", "queryParams")]
    public partial class ProductDetailPage : ContentPage
    {
        private string _queryParams;
        private IProductDetailDataService productDetailDataService;
        private IUserCartDataService userCartDataService;
        private IOrderDataService orderDataService;
        private QueryParamsDto jsonObj;
        public bool renderPageOnOnAppearing = false;


        public string QueryParams
        {
            get => _queryParams;
            set
            {
                _queryParams = (Uri.UnescapeDataString(value ?? string.Empty));

                OnPropertyChanged();
                productDetailDataService = DependencyService.Resolve<IProductDetailDataService>();
                userCartDataService = DependencyService.Resolve<IUserCartDataService>();
                orderDataService = DependencyService.Resolve<IOrderDataService>();
                jsonObj = JsonConvert.DeserializeObject<QueryParamsDto>(_queryParams);

                BindingContext = new ProductDetailViewModel(productDetailDataService, userCartDataService, orderDataService, jsonObj);
            }
        }


        public ProductDetailPage()
        {
            InitializeComponent();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing)
            {
                BindingContext = new ProductDetailViewModel(productDetailDataService, userCartDataService, orderDataService, jsonObj);
            }
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }
    }
}

