using System;
using System.Collections.Generic;
using System.Text;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Mobile.MockDataService;
using Xamarin.Forms;
using OrderDataService = TheOrganicShop.Mobile.DataService.OrderDataService;

namespace TheOrganicShop.Mobile
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(bool mockEnabled)
        {
            if (!mockEnabled)
            {
                DependencyService.Register<IOrderDataService, OrderDataService>();
                DependencyService.Register<IBannerDataService, BannerDataService>();
                DependencyService.Register<ICategoryDataService, DataService.CategoryDataService>();
                DependencyService.Register<IProductDetailDataService, DataService.ProductDetailDataService>();
                DependencyService.Register<IUserCartDataService, DataService.UserCartDataService>();
                DependencyService.Register<IUserDataService, DataService.UserDataService>();
                DependencyService.Register<IPaymentDataService, DataService.PaymentDataService>();
                DependencyService.Register<IDomainDataService, DataService.DomainDataService>();
                DependencyService.Register<INotificationDataService, DataService.NotificationDataService>();
                DependencyService.Register<ILocalStorage>();
            }
            else
            {
                DependencyService.Register<IBannerDataService, MockDataService.BannerDataService>();
                DependencyService.Register<IOrderDataService, MockDataService.OrderDataService>();
                DependencyService.Register<ICategoryDataService, MockDataService.CategoryDataService>();
                DependencyService.Register<IProductDetailDataService, MockDataService.ProductDetailDataService>();
                DependencyService.Register<IUserCartDataService, MockDataService.UserCartDataService>();
            }
           
        }
    }
}
