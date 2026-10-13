using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(
            INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _notificationRepository
                .GetUnreadCountAsync(userId);
        }
    }
}