using TheOrganicShop.Mobile.Extensions;
using TheOrganicShop.Mobile.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRendereriOS))]
namespace TheOrganicShop.Mobile.iOS
{
    
    public class CustomEntryRendereriOS : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.TintColor = UIColor.Black;
            }
        }
    }
}