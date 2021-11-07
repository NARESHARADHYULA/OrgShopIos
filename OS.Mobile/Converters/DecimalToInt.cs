using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.Converters
{
    [Preserve(AllMembers = true)]
    public class DecimalToInt: IValueConverter
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
            var data = value.ToString();
            return data.Substring(0, data.IndexOf('.') > 0 ? data.IndexOf('.') : data.Length);           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
