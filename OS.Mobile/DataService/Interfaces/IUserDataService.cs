using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TheOrganicShop.Models.Dtos.Payment;
using TheOrganicShop.Models.Dtos.UserAddress;
using TheOrganicShop.Models.Dtos.UserOtp;
using TheOrganicShop.Models.Dtos.UserWallet;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface IUserDataService
    {
        Task<int> AddUserAsync(CreateUserDto userDto);
        Task<bool> CreateAndSendUserOtpAsync(CreateUserOtpDto createUserOtpDto);
        Task<bool> ValidateUserOtpAsync(UserOtpValidateInputDto userOtpValidateInputDto);
        Task<GetUserWalletDtoMobileForView> GetUserWalletAsync(int userId);
        Task<List<GetUserWalletTransactionHistoryDtoForMobileView>> GetWalletTxHistoriesAsync(int userId);
        Task<ObservableCollection<GetUserAddressDtoMobileForView>> GetUserAddressAsync(int userId);
        Task<bool> AddOrUpdateUserAddressAsync(CreateOrEditUserAddressDtoMobile userAddress);
        void AddMoneyToWalletAsync(CreateOrEditUserWalletTransactionHistoryDto createOrEditUserWalletTransaction);
        Task AddMoneyToWalletByRazorPayAsync(PaymentDetailsDto paymentDetailsDto);
        Task<bool> AddToUserWaitListAsync(CreateUserWaitListDtoMobile waitListDto);
    }
}
