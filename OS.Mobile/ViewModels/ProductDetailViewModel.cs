using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Syncfusion.XForms.TabView;
using TheOrganicShop.Mobile.Commands;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.OrderDetail;
using TheOrganicShop.Models.Dtos.ProductDetail;
using TheOrganicShop.Models.Dtos.UserCart;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.ViewModels
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ProductDetailViewModel : BaseViewModel
    {

        private bool isLoading = false;
        public int maxNoOfItemsForProduct = 9;

        public bool IsLoading
        {
            get { return isLoading; }
            set
            {
                this.isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
        private int totalItems;

        /// <summary>
        /// Gets or sets the property that has been bound with label, which displays discount.
        /// </summary>
        public int TotalItems
        {
            get => totalItems;

            set
            {
                if (totalItems == value) return;

                totalItems = value;
                OnPropertyChanged();
            }
        }

        private readonly IProductDetailDataService productDetailDataService;
        private readonly IUserCartDataService cartDataService;
        private readonly IOrderDataService orderDataService;

        private QueryParamsDto _queryParamsDto { get; set; }

        private int totalOrderedItems = 0;
        private decimal totalPrice = 0;
       
        public ObservableCollection<GetProductDetailDtoMobileForView> Products { get; set; }

        private ObservableCollection<GetProductDetailDtoMobileForView> _productsForView { get; set; }

        public ObservableCollection<GetProductDetailDtoMobileForView> Orders { get; set; }
        public ObservableCollection<GetOrderDetailDtoMobileForView> PlacedOrders { get; set; }
        private ObservableCollection<GetOrderDetailDtoMobileForView> placedOrders { get; set; }
        public List<GetProductDetailDtoMobileForView> orders { get; set; }

        public Command<object> AddCommand { get; set; }

        public Command<object> LoadMoreItemsCommand { get; set; }

        public Command<object> OrderListCommand { get; set; }

        public Command<object> RemoveOrderCommand { get; set; }

        public Command<GetProductDetailDtoMobileForView> AddQuantityCommand => new Command<GetProductDetailDtoMobileForView>((item) =>
        {
            if (item.Quantity < maxNoOfItemsForProduct)
            {
                item.Quantity++;
                AddItemsToCart(item);

            }
        });

        public Command<GetProductDetailDtoMobileForView> RmQuantityCommand => new Command<GetProductDetailDtoMobileForView>((item) =>
        {
            if (item.Quantity > 0)
            {
                item.Quantity--;
                AddItemsToCart(item);
            }
                
            
        });

        private DelegateCommand backButtonCommand;
        

        public ICommand SearchCommand { get; set; }

        public ObservableCollection<GetProductDetailDtoMobileForView> SearchResults { get; private set; }

        public ObservableCollection<GetProductDetailDtoMobileForView> ProductsForView
        {
            get
            {
                return _productsForView;
            }
            set
            {
                _productsForView = value;
                OnPropertyChanged("ProductsForView");
            }
        }


        public Command CheckoutCommand { get; set; }

        public SfTabView tabView;
        public int TotalOrderedItems
        {
            get { return totalOrderedItems; }
            set
            {
                if (totalOrderedItems != value)
                {
                    totalOrderedItems = value;
                    OnPropertyChanged("TotalOrderedItems");
                }
            }
        }

        public decimal TotalPrice
        {
            get { return totalPrice; }
            set
            {
                if (totalPrice != value)
                {
                    totalPrice = value;
                    OnPropertyChanged("TotalPrice");
                }
            }
        }

        

        public ProductDetailViewModel(IProductDetailDataService productDetailDataService, IUserCartDataService cartDataService, IOrderDataService orderDataService,  QueryParamsDto queryParamsDto)
        {
            IsLoading = true;
            this.cartDataService = cartDataService;
            this.productDetailDataService = productDetailDataService;
            this.orderDataService = orderDataService;
            _queryParamsDto = queryParamsDto;
            Products = new ObservableCollection<GetProductDetailDtoMobileForView>();
            
            Device.InvokeOnMainThreadAsync(async () =>
           {
               var cartItems = await cartDataService.GetCartItemAsync(App.UserId);
               await GetOrderDetailsByUserIdAndOrderedDate(App.UserId, Convert.ToDateTime(queryParamsDto.Date));
               TotalOrderedItems = cartItems.Sum(x => x.CartItems.Count);
               GetOrders(cartItems);
               Orders = new ObservableCollection<GetProductDetailDtoMobileForView>(orders);
               await GetProducts(_queryParamsDto.CategoryId);

           });

            AddCommand = new Command<object>(AddQuantity);
            CheckoutCommand = new Command(CheckOut);
            RemoveOrderCommand = new Command<object>(RemoveOrder);
        }

        private void RemoveOrder(object obj)
        {
            var p = obj as GetProductDetailDtoMobileForView;
            p.Quantity = 0;
        }

        private async void CheckOut(object obj)
        {

            await Shell.Current.GoToAsync("usercart");

        }

        public async Task AddItemsToCart(GetProductDetailDtoMobileForView item)
        {
            var createOrEditUserCartDtoMobile = new CreateOrEditUserCartDtoMobile
            {
                UserId = App.UserId,
                ProductDetailId = item.Id,
                OrderingForDate = Convert.ToDateTime(_queryParamsDto.Date),
                Quantity = item.Quantity,
            };

            var currentOrder = Orders.FirstOrDefault(o => o.Id == item.Id);
            if (currentOrder != null && item.Quantity <= 0)
            {
                // remove item
                createOrEditUserCartDtoMobile.CreatedDateTime = DateTime.Now;
                createOrEditUserCartDtoMobile.CreatedByUserId = App.UserId;
                Orders.Remove(currentOrder);
                await cartDataService.RemoveCartItemAsync(createOrEditUserCartDtoMobile);
                TotalOrderedItems--;
            }// if item is adding for the first time
            else if (currentOrder == null && item.Quantity > 0)
            {
                createOrEditUserCartDtoMobile.CreatedDateTime = DateTime.Now;
                createOrEditUserCartDtoMobile.CreatedByUserId = App.UserId;
                await cartDataService.AddCartItemAsync(createOrEditUserCartDtoMobile);
                Orders.Add(item);
                TotalOrderedItems++;
            }
            else
            {
                // update the item quantity
                createOrEditUserCartDtoMobile.ModifiedDateTime = DateTime.Now;
                createOrEditUserCartDtoMobile.ModifiedByUserId = App.UserId;
                await cartDataService.UpdateCartItemAsync(createOrEditUserCartDtoMobile);
            }
            
            TotalPrice = 0;
            TotalItems = Orders.Count;
            foreach(var order in Orders)
            {
                TotalPrice += order.DiscountedPrice * order.Quantity;
            }
            
        }

        public async Task  GetOrderDetailsByUserIdAndOrderedDate(int userId, DateTime orderDate)
        {
            var placedProducts = await orderDataService.GetOrderDetailByDateAndUserForMobileAsync(userId,orderDate);
            PlacedOrders = (placedProducts == null) ? new ObservableCollection<GetOrderDetailDtoMobileForView>() : new ObservableCollection<GetOrderDetailDtoMobileForView>(placedProducts);

        }

        // 1 update cart count
        // 2 update current day cart items count and price
        
        private void GetOrders(List<GetUserCartCalenderDtoMobileForView> cartItems)
        {
            orders = new List<GetProductDetailDtoMobileForView>();
            var currentDateItems = cartItems
               .FirstOrDefault(x => Convert.ToDateTime(x.Date).Date == Convert.ToDateTime(_queryParamsDto.Date).Date);
            if (currentDateItems != null)
            {
                foreach (var item in currentDateItems.CartItems)
                {
                    TotalPrice += item.DiscountedPrice * item.Quantity;
                    orders.Add(new GetProductDetailDtoMobileForView { Id = item.ProductDetailId, Quantity = item.Quantity, OrderingForDate = Convert.ToDateTime(_queryParamsDto.Date).Date, DiscountedPrice = item.DiscountedPrice });
                }
                TotalItems = currentDateItems.CartItems.Count();
            }
        }

        public ObservableCollection<GetProductDetailDtoMobileForView> updateProductsModel(ObservableCollection<GetProductDetailDtoMobileForView> products)
        {
            // if query param date is less than or equal to today then cutoff reached true
            // if query param date is tomorrow check for IsCutOffTimeReached
            // if query param is greater than tomorrow then directly IsCutOffTimeReached to false
            foreach (var product in products)
            {
                var orderItem = Orders.FirstOrDefault(orders => orders.Id == product.Id);
                var orderedProduct = PlacedOrders.FirstOrDefault(p => p.ProductId == product.Id);
                product.IsCutOffTimeReached =
                   DateTime.ParseExact(_queryParamsDto.Date, "yyyy/MM/dd", CultureInfo.InvariantCulture).Date <
                       (DateTime.Now.AddDays(1).Date) && product.IsCutOffTimeReached;
                product.Quantity = (orderItem == null ? product.Quantity : orderItem.Quantity);
                product.Note = orderedProduct == null ? "" 
                    :(orderedProduct.Quantity == 1
                        ? $"{orderedProduct.Quantity} Quantity placed for the day"
                        :$"{orderedProduct.Quantity} Quantities placed for the day");
                
            }


            return products;
        }

       
        private async Task GetProducts(int categoryId)
        {
            var products  = await productDetailDataService.GetProductsForMobileByCategoryAsync(categoryId);
            products = updateProductsModel(products);
            Products = new ObservableCollection<GetProductDetailDtoMobileForView>(products);
            IsLoading = false;
        }

        private void AddQuantity(object obj)
        {
            var p = obj as GetProductDetailDtoMobileForView;
            p.Quantity += 1;
        }

    }
}
