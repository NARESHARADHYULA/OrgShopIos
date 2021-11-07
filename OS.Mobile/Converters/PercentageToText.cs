using System;
using System.Globalization;
using TheOrganicShop.Models.Dtos.ProductDetail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.Converters
{
    [Preserve(AllMembers = true)]
    public class PercentageToText:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percentage = value.ToString();
            if (percentage.StartsWith("0"))
            {
                return "";
            }
            var percentageTrimmed = percentage.Substring(0, percentage.IndexOf('.') > 0 ? percentage.IndexOf('.') : percentage.Length);
            return $" {percentageTrimmed}% Off )";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
