using System;

namespace TheOrganicShop.Tools
{
    public static class DateTimeExtensions
    {
        public static string AsFormattedString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }
    }
}
