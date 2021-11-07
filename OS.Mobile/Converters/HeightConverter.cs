using Syncfusion.ListView.XForms;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using TheOrganicShop.Models.Dtos.Order;
using Xamarin.Forms;

namespace TheOrganicShop.Mobile.Converters
{
    public class HeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var listView = parameter as SfListView;
            var items = value as ObservableCollection<GetOrderSummaryDtoMobileForView>;
            if (items == null) { return 100; }
            return items.Count * listView.ItemSize;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
