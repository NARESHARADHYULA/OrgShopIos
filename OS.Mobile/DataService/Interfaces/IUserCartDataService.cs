using System.Collections.Generic;
using System.Threading.Tasks;
using TheOrganicShop.Models.Data;
using TheOrganicShop.Models.Dtos;
using TheOrganicShop.Models.Dtos.Order;
using TheOrganicShop.Models.Dtos.ProductDetail;
using TheOrganicShop.Models.Dtos.UserCart;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface IUserCartDataService
    {

        Task<List<GetUserCartCalenderDtoMobileForView>> GetCartItemAsync(int userId);
        Task<int> GetCountByUserIdForMobileAsync(int userId);
        Task<bool> AddCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto);
        Task<bool> UpdateCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto);
        Task<int> UpdateByIdAsync(int id, int quantity);
        Task<bool> RemoveCartItemAsync(CreateOrEditUserCartDtoMobile userCartDto);
        Task<bool> RemoveCartItemByUserIdAsync(int userId);
        Task<List<GetUserCartCalenderDtoMobileForView>> GetOrderedItemsAsync(int userId);
        Task<int> DeleteByIdAsync(int cartId);
    }
}
