using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace TheOrganicShop.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {

        //public static string BaseUri = "http://192.168.194.241:44350/api/";
        //  public static string BaseUri = "http://orgshop-api.azurewebsites.net/api/";

        public static string BaseUri = "https://theorganicshop-mobile-api.azurewebsites.net/api/";
        public static bool MockDataService = false;

        public static int UserId = 1;
        public static string UseName = "The Org Shop";
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTMwNDIwQDMxMzkyZTMzMmUzMFluNll6aVREN0tjdjRVV2VSNFBSaHJPMHBCUS8xdVNuUVI4Q2d3VnkvSE09");
            InitializeComponent();
            ServiceRegistration.AddInfrastructure(MockDataService);
            if (!LocalStorage.Shared.Initialized) LocalStorage.Shared.Init();
            GetUserInfo();
           
        }


        protected override void OnStart()
        {
            AppCenter.Start("android=dff755d5-3399-4183-ae01-45e5b407ac07;" +
                  "ios=8ba2e8e6-4f57-42dd-8178-0251cbdb0962;",
                  typeof(Analytics), typeof(Crashes));
            Analytics.TrackEvent("App Started");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void GetUserInfo()
        {
            var userInfo = LocalStorage.Shared.GetUserInfo();
            if (userInfo != null)
            {
                UserId = userInfo.UserId;
                UseName = userInfo.UserName;
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new DeliveryAreaVerification();
            }
        }
    }
}
