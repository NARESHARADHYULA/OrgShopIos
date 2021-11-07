using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.Extensions;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CurrentDate", "currentDate")]
    public partial class CategoryPage : ContentPage
    {
        public ICommand NavigateCommand => new Command(Navigate);
        public bool renderPageOnOnAppearing = false;
        private string _currentDate;
        public ICategoryDataService categoryDataService;
        public string CurrentDate
        {
            get => _currentDate;

            set
            {
                _currentDate = Uri.UnescapeDataString(value ?? string.Empty);
                OnPropertyChanged();
                categoryDataService = DependencyService.Resolve<ICategoryDataService>();
                BindingContext = new CategoryPageViewModel(categoryDataService, App.UserId, _currentDate);
            }
        }

        public CategoryPage()
        {
            InitializeComponent();

        }

        private async void Navigate(object route)
        {
            await Shell.Current.GoToAsync("..");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (renderPageOnOnAppearing) {
                BindingContext = new CategoryPageViewModel(categoryDataService, App.UserId, _currentDate);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            renderPageOnOnAppearing = true;
        }
    }
}