using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TheOrganicShop.Models.Annotations;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.UserCart
{

    public class GetUserCartDtoMobileForView : INotifyPropertyChanged
    {
        private int quantity = 0;
        public int Id { get; set; }

        public DateTime OrderingForDate { get; set; }

        public string ImageUri { get; set; }

        public int ProductId { get; set; }

        public int ProductDetailId { get; set; }

        public string ProductName { get; set; }

        public string Weight { get; set; }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;

                    OnPropertyChanged("Quantity");
                }
            }
        }

        public decimal OriginalPrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal DiscountedPrice { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
