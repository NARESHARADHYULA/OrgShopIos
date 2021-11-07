using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using TheOrganicShop.Mobile.DataService;
using TheOrganicShop.Mobile.DataService.Interfaces;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos.Category;
using Xamarin.Forms.Internals;

namespace TheOrganicShop.Mobile.MockDataService
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class CategoryDataService : ICategoryDataService
    {
        #region Constructor

        #endregion

        #region Fields

        private CategoryDataService dataService;

        #endregion

        #region Properties

        [DataMember(Name = "categories")]
        public List<GetCategoryDtoMobileForView> Categories { get; set; }


        public CategoryDataService DataService =>
            dataService = PopulateData<CategoryDataService>("orgshop.json");

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

        #endregion

        public async Task<List<GetCategoryDtoMobileForView>> GetCategoriesAsync()
        {
            return await Task.FromResult(DataService?.Categories);
        }
    }
}
