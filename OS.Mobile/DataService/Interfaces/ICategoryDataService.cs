using System.Collections.Generic;
using System.Threading.Tasks;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos.Category;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface ICategoryDataService
    {
        Task<List<GetCategoryDtoMobileForView>> GetCategoriesAsync();
    }
}