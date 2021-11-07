using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views
{
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderCalenderPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderCalenderPage" /> class.
        /// </summary>
        public OrderCalenderPage()
        {
            InitializeComponent();
            var orderCalenderDataService = new MockDataService.OrderDataService();
            BindingContext = new OrderCalenderViewModel(orderCalenderDataService, 1);
        }

    }
}