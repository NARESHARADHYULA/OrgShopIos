using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using TheOrganicShop.Models.Annotations;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.UserCart
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetUserCartCalenderDtoMobileForView : INotifyPropertyChanged
    {
        [DataMember(Name = "date")]
        public string Date { get; set; }

        public string FormattedDate  {
            get { return DateTime.Parse(Date).ToString("dd/MM/yyyy"); }
        }
        public DateTime FormattedDateObj
        {
            get { return DateTime.Parse(Date); }
        }

        private ObservableCollection<GetUserCartDtoMobileForView> _cartItems { get; set; }

        [DataMember(Name = "cartitems")]
        public ObservableCollection<GetUserCartDtoMobileForView> CartItems
        {
            get { return _cartItems; }
            set
            {
                _cartItems = value;
                this.OnPropertyChanged("CartItems");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
