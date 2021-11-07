using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System.Linq;
using System.Reflection;

namespace TheOrganicShop.Mobile.Controls
{
    public class ExtendedListView : SfListView
    {
        VisualContainer container;
        public ExtendedListView()
        {
            container = this.GetVisualContainer();
            container.PropertyChanged += Container_PropertyChanged;
        }

        private void Container_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Height))
            {
                var extent = (double)container.GetType().GetRuntimeProperties().FirstOrDefault(container => container.Name == "TotalExtent").GetValue(container);
                this.HeightRequest = 300;
            }
        }
    }
}
