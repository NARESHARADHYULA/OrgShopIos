using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views.ReusableViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalenderView : StackLayout
    {
        public CalenderView()
        {
            InitializeComponent();
        }
    }
}