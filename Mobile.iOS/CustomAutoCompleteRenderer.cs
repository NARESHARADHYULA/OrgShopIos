using Syncfusion.SfAutoComplete.XForms;
using Syncfusion.SfAutoComplete.XForms.iOS;
using TheOrganicShop.Mobile.Extensions;
using TheOrganicShop.Mobile.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomAutoComplete), typeof(CustomAutoCompleteRenderer))]
namespace TheOrganicShop.Mobile.iOS
{
    public class CustomAutoCompleteRenderer : SfAutoCompleteRenderer
    {
        public CustomAutoCompleteRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SfAutoComplete> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.TintColor = UIColor.Black;
            }
        }
    }
	/// <summary>
	/// Represents the auto complete material renderer class.
	/// </summary>
	internal class MaterialSfAutoCompleteRenderer : SfAutoCompleteRenderer
	{
		public MaterialSfAutoCompleteRenderer()
		{

		}
	}

}