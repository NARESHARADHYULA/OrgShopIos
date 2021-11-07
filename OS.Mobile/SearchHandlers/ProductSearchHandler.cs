using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Syncfusion.DataSource.Extensions;
using TheOrganicShop.Models.Dtos.ProductDetail;
using Xamarin.Forms;

namespace TheOrganicShop.Mobile.SearchHandlers
{
    public class ProductSearchHandler: SearchHandler
    {
        public List<GetProductDetailDtoMobileForView> ProductDetails { get; set; }
        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = ItemsSource.ToList<GetProductDetailDtoMobileForView>()
                    .Where(pd => pd.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<GetProductDetailDtoMobileForView>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);


            // Note: strings will be URL encoded for navigation (e.g. "Blue Monkey" becomes "Blue%20Monkey"). Therefore, decode at the receiver.
            await Shell.Current.GoToAsync($"productdetailpage?id={((GetProductDetailDtoMobileForView)item).Id}");
        }
    }
}
