using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Category;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.ProductDetail;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.MockDataService
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ProductDetailDataService : IProductDetailDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private ProductDetailDataService dataService;

        #endregion

        #region Properties

        [DataMember(Name = "products")]
        public List<GetProductDetailDtoMobileForView> Products { get; set; }


        public ProductDetailDataService DataService =>
            dataService = PopulateData<ProductDetailDataService>("orgshop.json");

        #endregion

        #region Methods

        
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


        public Task<ObservableCollection<GetProductDetailDtoMobileForView>> GetProductsForMobileByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<GetProductDetailDtoMobileForView>> GetProductsForMobileBySubCategoryAsync(int subCategoryId)
        {
            return await Task.FromResult(new ObservableCollection<GetProductDetailDtoMobileForView>(DataService?.Products));
        }

        #endregion
    }
}
