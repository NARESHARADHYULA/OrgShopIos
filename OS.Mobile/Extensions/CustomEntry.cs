using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TheOrganicShop.Mobile.Extensions
{
    public class CustomEntry : Xamarin.Forms.Entry
    {
        public CustomEntry()
        {
            base.TextColor = Color.FromHex("#FF56565A");
        }
    }
}
