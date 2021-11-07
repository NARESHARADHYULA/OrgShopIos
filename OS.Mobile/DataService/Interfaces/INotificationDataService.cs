using System.Threading.Tasks;
using TheOrganicShop.Models.Dtos.Notification;

namespace TheOrganicShop.Mobile.DataService.Interfaces
{
    public interface INotificationDataService
    {
        Task<bool> SendNotificationAsync(NotificationInputDto notificationInputDto);
    }
}
