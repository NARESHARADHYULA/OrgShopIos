using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Syncfusion.SfAutoComplete.XForms.Droid;
using TheOrganicShop.Mobile.Droid;
using TheOrganicShop.Mobile.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomAutoComplete), typeof(CustomSfAutoCompleteRendererAndroid))]
namespace TheOrganicShop.Mobile.Droid
{
    public class CustomSfAutoCompleteRendererAndroid : SfAutoCompleteRenderer
    {
        public CustomSfAutoCompleteRendererAndroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Syncfusion.SfAutoComplete.XForms.SfAutoComplete> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                JNIEnv.SetField(Control.GetAutoEditText().Handle, mCursorDrawableResProperty, Resource.Drawable.my_cursor);
            }
        }
    }
}