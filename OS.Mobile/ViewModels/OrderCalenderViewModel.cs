using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Order;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class OrderCalenderViewModel:BaseViewModel
    {
        #region Public properties

        public ObservableCollection<GetOrderCalenderInfoDtoMobileForView> OrderCalenderInfoItems
        {
            get => orderCalenderInfoItems;
            set
            {
                if (orderCalenderInfoItems == value) return;

                orderCalenderInfoItems = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Fields

        private ObservableCollection<GetOrderCalenderInfoDtoMobileForView> orderCalenderInfoItems;
        private readonly IOrderDataService orderCalenderInfoDataService;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OrderCalenderViewModel" /> class.
        /// </summary>
        public OrderCalenderViewModel(IOrderDataService orderCalenderInfoDataService, int userId)
        {
            this.orderCalenderInfoDataService = orderCalenderInfoDataService;

            Device.BeginInvokeOnMainThread(() => { FetchOrderCalenderInfoItems(userId); });
        }

        #endregion

        public async void FetchOrderCalenderInfoItems(int userId)
        {
            try
            {
                var orderCalenderInfoItems = await orderCalenderInfoDataService.GetOrderCalenderInfoForMobileAsync(userId);
                if (orderCalenderInfoItems != null && orderCalenderInfoItems.Count > 0)
                {
                    OrderCalenderInfoItems = new ObservableCollection<GetOrderCalenderInfoDtoMobileForView>(orderCalenderInfoItems);
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void BackButtonClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

    }
}
