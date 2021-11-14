using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Newtonsoft.Json;
using Org.Json;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos.Payment;
using TheOrganicShop.Models.Dtos.UserWallet;
using TheOrganicShop.Models.Enums;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Acr.UserDialogs;
using AllInOneSDK;
using Android.Content;

namespace TheOrganicShop.Mobile.Droid
{
    [Activity(Label = "TheOrganicShop.Mobile", ScreenOrientation = ScreenOrientation.Portrait, Icon = "@drawable/Icon", Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // Name of the MainActivity theme you had there before.
            // Or you can use global::Android.Resource.Style.ThemeHoloLight
            base.SetTheme(Resource.Style.MainTheme);
            
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            Rg.Plugins.Popup.Popup.Init(this);
            UserDialogs.Init(this);
            LoadApplication(new App());

            //MessagingCenter.Subscribe<UserPaymentInputDto>(this, "AddMoney", async (paymentInput) =>
            //{
            //    await PayViaRazor(paymentInput);
            //    await PopupNavigation.Instance.PopAsync();
            //});
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            AllInOnePlugin.OnActivityResult(requestCode, resultCode, data);
        }

        //public void OnPaymentError(int p0, string p1, PaymentData p2)
        //{
        //    var userService = Xamarin.Forms.DependencyService.Resolve<IUserDataService>();

        //    //TODO: Handle the case of user clicking cancel before proceeding to payment section
        //    if (p2 != null)
        //    {
        //        userService.AddMoneyToWalletByRazorPayAsync(new PaymentDetailsDto
        //        {
        //            PaymentId = p2.PaymentId,
        //            OrderId = p2.OrderId,
        //            PaymentStatus = PaymentStatus.Failure.ToString()
        //        });
        //        MessagingCenter.Send("AddMoney", "Refresh_Wallet");
        //    }

        //}

        //public void OnPaymentSuccess(string p0, PaymentData p1)
        //{
        //    var userService = Xamarin.Forms.DependencyService.Resolve<IUserDataService>();

        //    userService.AddMoneyToWalletByRazorPayAsync(new PaymentDetailsDto
        //    {
        //        PaymentId = p1.PaymentId,
        //        OrderId = p1.OrderId,
        //        PaymentSignature = p1.Signature,
        //        PaymentStatus = PaymentStatus.Success.ToString()
        //    });
        //    MessagingCenter.Send("AddMoney", "Refresh_Wallet");
        //}

        //public async Task PayViaRazor(UserPaymentInputDto payload)
        //{
        //    var paymentDataService = Xamarin.Forms.DependencyService.Resolve<IPaymentDataService>();

        //    var paymentResponseDto = await paymentDataService.CreateRazorPayOrderAsync(payload);

        //    if (!string.IsNullOrEmpty(paymentResponseDto.Id))
        //    {
        //        Checkout checkOut = new Checkout();
        //        checkOut.SetKeyID("rzp_test_bJcXs67wrN2DWk");
        //        Activity activity = this;
        //        try
        //        {
        //            JSONObject options = new JSONObject();
        //            options.Put("description", "Add balance");
        //            options.Put("order_id", paymentResponseDto.Id);
        //            options.Put("currency", paymentResponseDto.Currency);
        //            options.Put("amount", paymentResponseDto.Amount.ToString(CultureInfo.InvariantCulture));
        //            checkOut.Open(activity, options);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("error in payment");
        //        }
        //    }
        //    else
        //    {
        //        Toast.MakeText(this, "Payment Error", ToastLength.Short).Show();
        //    }
        //}


    }
}