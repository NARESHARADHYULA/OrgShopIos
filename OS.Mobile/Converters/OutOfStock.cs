using System;
using System.Globalization;
using TheOrganicShop.Models.Dtos.ProductDetail;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.Converters
{
    [Preserve(AllMembers = true)]
    public class OutOfStock: IValueConverter
    {
        /// <summary>
        /// This method is used to convert the int to bool.
        /// </summary>
        /// <param name="value">Gets the value.</param>
        /// <param name="targetType">Gets the target type.</param>
        /// <param name="parameter">Gets the parameter.</param>
        /// <param name="culture">Gets the culture.</param>
        /// <returns>Returns the bool.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sender = parameter as Label;
            var data = sender.BindingContext as GetProductDetailDtoMobileForView;
            if (data.IsCutOffTimeReached)
            {
                sender.Text = "Cutoff time reached";
                data.AvailableQuantity = 0;
                return true;

            }
            else if (data.AvailableQuantity ==0)
            {
                sender.Text = "Out off stock";
                return true;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
