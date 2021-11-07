using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.DomainData
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetCalenderDtoMobileForView : INotifyPropertyChanged
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "stringdate")]
        public string StringDate { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "day")]
        public int Day { get; set; }

        [DataMember(Name = "month")]
        public int Month { get; set; }

        [DataMember(Name = "year")]
        public int Year { get; set; }

        [DataMember(Name = "dayshortname")]
        public string DayShortName { get; set; }

        [DataMember(Name = "monthshortname")]
        public string MonthShortName { get; set; }

        [DataMember(Name = "dayfullname")]
        public string DayFullName { get; set; }

        [DataMember(Name = "monthfullname")]
        
        public string MonthFullName { get; set; }
        
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
