using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos.ProductDetail;
using TheOrganicShop.Models.Dtos.UserCart;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.MockDataService
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class UserCartDataService: IUserCartDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private UserCartDataService dataService;

        private readonly List<GetUserCartCalenderDtoMobileForView> cartItems = new List<GetUserCartCalenderDtoMobileForView>();

        private readonly List<GetUserCartCalenderDtoMobileForView> orderedItems = new List<GetUserCartCalenderDtoMobileForView>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the property, which displays the collection of products from json.
        /// </summary>
        [DataMember(Name = "products")]
        public List<GetProductDetailDtoMobileForView> Products { get; set; }

        /// <summary>
        /// To pouplate the json file.
        /// </summary>
        public UserCartDataService DataService =>
            dataService = PopulateData<UserCartDataService>("orgshop.json");

        #endregion

        #region Methods

        public Task<int> GetCountByUserIdForMobileAsync(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// To add the cart item.
        /// </summary>
        public async Task<bool> AddCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto)
        {

            var status = false;
            try
            {
                
                var isProductAlreadyAdded = cartItems.Any(s => s.CartItems.Any(cartitem => cartitem.ProductDetailId == userCartDto.ProductDetailId));
                if (!isProductAlreadyAdded)
                {
                   var item = DataService?.Products.FirstOrDefault(x => x.Id == userCartDto.ProductDetailId);
                   var cartItem = new GetUserCartDtoMobileForView
                   {
                       ProductDetailId = userCartDto.ProductDetailId,
                       Quantity = 1,
                       DiscountedPrice = item.DiscountedPrice,
                       OriginalPrice = item.ActualPrice,
                       ProductName = item.Name
                   };
                   
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception ex)
            {
                status = false;
                
            }

            return await Task.FromResult(status);
        }

        public async Task<bool> UpdateCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateByIdAsync(int id, int quantity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveCartItemByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get cart item from database using web API.
        /// </summary>
        public async Task<List<GetUserCartCalenderDtoMobileForView>> GetCartItemAsync(int userId)
        {
            return await Task.FromResult(cartItems);
        }

        /// <summary>
        /// This method is to remove the cart item.
        /// </summary>
        public async Task<bool> RemoveCartItemAsync(int userId, int productDetailId)
        {
            var status = false;
            //try
            //{
            //    var item = cartItems.FirstOrDefault(s => s.ProductDetailId == productDetailId);
            //    if (item != null) cartItems.Remove(item);

            //    status = true;
            //}
            //catch (Exception ex)
            //{
            //    status = false;
               
            //}

            return await Task.FromResult(status);
        }

        /// <summary>
        /// This method is to remove the cart items.
        /// </summary>
        public async Task<bool> RemoveCartItemsAsync(int userId)
        {
            var status =false;
            try
            {
                foreach (var item in cartItems.ToList())
                    orderedItems.Add(item);

                foreach (var item in cartItems.ToList()) cartItems.Remove(item);

                status = true;
            }
            catch (Exception ex)
            {
                status = false;
               
            }

            return await Task.FromResult(status);
        }

        /// <summary>
        /// To update the product quantity.
        /// </summary>
        public async Task<bool> UpdateQuantityAsync(int userId, int productDetailId, int quantity)
        {
            var status = false;
            //try
            //{
            //    cartItems.Where(s => s.ProductDetailId == productDetailId).Select(c =>
            //    {
            //        c.Quantity = quantity;
            //        return c;
            //    });
            //    status = true;
            //}
            //catch (Exception ex)
            //{
            //    status = false;
               
            //}

            return await Task.FromResult(status);
        }

        /// <summary>
        /// Populates the data from json file.
        /// </summary>
        /// <param name="fileName">Json file to fetch data.</param>
        private static T PopulateData<T>(string fileName)
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;

            T obj;

            using (Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.MockData.{fileName}"))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                obj = (T)serializer.ReadObject(stream);
            }

            return obj;
        }

        public async Task<List<GetUserCartCalenderDtoMobileForView>> GetOrderedItemsAsync(int userId)
        {
            return await Task.FromResult(orderedItems);
        }

        public Task<int> DeleteByIdAsync(int cartId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
