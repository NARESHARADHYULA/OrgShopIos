using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TheOrganicShop.Models.Annotations;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Models.Dtos.ProductDetail
{
    [DataContract]
    [Preserve(AllMembers = true)]
    public class GetProductDetailDtoMobileForView: INotifyPropertyChanged
    {
        private int quantity = 0;

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "productId")]
        public int ProductId { get; set; }

        [IgnoreDataMember]
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

        [IgnoreDataMember]
        public DateTime OrderingForDate { get; set; }

        [IgnoreDataMember]
        public string Note { get; set; }

        [DataMember(Name = "imageuri")]
        public string ImageUri { get; set; }

        [DataMember(Name = "totalquantity")]
        public int TotalQuantity { get; set; }

        [DataMember(Name = "availablequantity")]
        public int AvailableQuantity { get; set; }

        [DataMember(Name = "iscutofftimereached")]
        public bool IsCutOffTimeReached { get; set; }

        [DataMember(Name = "weight")]
        public string Weight { get; set; }

        [DataMember(Name = "actualprice")]
        public decimal ActualPrice { get; set; }

        [DataMember(Name = "discountpercent")]
        public decimal? DiscountPercent { get; set; }

        [DataMember(Name = "discountedprice")]
        public decimal DiscountedPrice { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
