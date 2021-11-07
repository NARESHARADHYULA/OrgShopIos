using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.Order
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetOrderCalenderInfoDtoMobileForView : INotifyPropertyChanged
    {

        private string orderDate { get; set; }

        [DataMember(Name = "orderdate")]
        public string OrderDate
        {
            get
            {
                return orderDate;
            }
            set
            {
                orderDate = value;
                OrderDateForDisplay = orderDate.Split('/')[2];

            }
        }

        [DataMember(Name = "OrderDateForDisplay")]
        public string OrderDateForDisplay { get; set; }


        [DataMember(Name = "ordercutofftime")]
        public string OrderCutOffTime { get; set; }

        [DataMember(Name = "iscutofftimereached")]
        public bool IsCutOffTimeReached { get; set; }

        [DataMember(Name = "placedanyorder")]
        public bool PlacedAnyOrder { get; set; }
        [DataMember(Name = "dayshortname")]
        public string DayShortName { get; set; }

        [DataMember(Name = "monthshortname")]
        public string MonthShortName { get; set; }

        [DataMember(Name = "day")]
        public int Day { get; set; }

        [DataMember(Name = "orderallowed")]
        public bool OrderAllowed { get; set; }

        Color textColor = Color.FromHex("#FF56565A");

        public Color LabelTextColor
        {
            get
            {
                return textColor;
            }
            set
            {
                textColor = value;
                this.RaisedOnPropertyChanged("LabelTextColor");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisedOnPropertyChanged(string _PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(_PropertyName));
            }
        }


    }
}
