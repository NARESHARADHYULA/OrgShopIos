using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPlaced : ContentPage
    {
        public OrderPlaced()
        {
            InitializeComponent();
            
        }

       
        private async void goToHome_Command(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell();
        }

        private async void goToOrders_Command(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("myorders?showOrdersForDate=");
        }
        

    }
}