using System;
using System.Globalization;
using TheOrganicShop.Models.Dtos.UserCart;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
namespace TheOrganicShop.Mobile.Converters
{
    [Preserve(AllMembers = true)]
    public class ActualPriceUserCart : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sender = parameter as Label;
            var data = sender.BindingContext as GetUserCartDtoMobileForView;
            var percentage = data.DiscountPercent.ToString();
            if (percentage.StartsWith("0"))
            {
                return "";
            }
            var actualPrice = data.OriginalPrice.ToString();
            var actualPriceTrimmed = actualPrice.Substring(0, actualPrice.IndexOf('.') > 0 ? actualPrice.IndexOf('.') : actualPrice.Length);
            return $"{actualPriceTrimmed}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
