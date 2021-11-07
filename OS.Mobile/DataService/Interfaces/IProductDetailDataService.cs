using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.ProductDetail;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface IProductDetailDataService
    {
        Task<ObservableCollection<GetProductDetailDtoMobileForView>> GetProductsForMobileByCategoryAsync(int categoryId);
        Task<ObservableCollection<GetProductDetailDtoMobileForView>> GetProductsForMobileBySubCategoryAsync(int subCategoryId);
      
    }
}
