using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.Converters
{
    [Preserve(AllMembers = true)]
    public class ZeroToEmpty : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = value.ToString();
            if (data.StartsWith("0"))
            {
                return "";
            }
           
            return $" ( {data.Substring(0, data.IndexOf('.') > 0 ? data.IndexOf('.') : data.Length)}% )";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
