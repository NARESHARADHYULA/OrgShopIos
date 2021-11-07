using System.Threading.Tasks;
using TheOrganicShop.Models.Dtos.Payment;
using TheOrganicShop.Models.Dtos.Payment.Paytm;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface IPaymentDataService
    {
        PaytmTransactionResponse InitiateTransactionAsync(PaymentInputDto paymentInputDto);
        bool CreatePaytmPaymentOrderAsync(PaytmPaymentResponseDto paymentResponseDto);
        Task<PaymentResponseDto> CreateRazorPayOrderAsync(UserPaymentInputDto paymentInputDto);
    }
}
