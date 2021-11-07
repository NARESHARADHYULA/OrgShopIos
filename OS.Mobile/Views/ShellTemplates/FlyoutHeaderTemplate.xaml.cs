using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TheOrganicShop.Mobile.Views.ShellTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeaderTemplate : Grid
    {
        public FlyoutHeaderTemplate()
        {
            InitializeComponent();
            name.Text = App.UseName;
        }
    }
}