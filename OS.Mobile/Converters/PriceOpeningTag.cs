using System;
using System.Globalization;
using TheOrganicShop.Models.Dtos.ProductDetail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
namespace TheOrganicShop.Mobile.Converters
{
    [Preserve(AllMembers = true)]
    public class PriceOpeningTag : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sender = parameter as Label;
            var data = sender.BindingContext as GetProductDetailDtoMobileForView;
            var percentage = data.DiscountPercent.ToString();
            if (percentage.StartsWith("0"))
            {
                return "";
            }
            return $" ( ";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
