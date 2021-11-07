using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace TheOrganicShop.Mobile.Converters
{
    public class SourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Device.RuntimePlatform == Device.Android)
                return value;

            if (value != null)
                return ImageSource.FromStream(() => new MemoryStream(new WebClient().DownloadData(value.ToString())));

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
