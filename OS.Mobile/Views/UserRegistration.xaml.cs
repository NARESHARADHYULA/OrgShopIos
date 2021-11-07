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
    public partial class UserRegistration : ContentPage
    {
       
        public UserRegistration(string mobileNo,string name)
        {
            InitializeComponent();
            var userDataService = DependencyService.Resolve<IUserDataService>();
            BindingContext = new UserRegistrationViewModel(userDataService, mobileNo,name);
        }
    }
}
