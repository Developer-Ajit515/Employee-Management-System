using ERP.DOMAINMODELS.Dtos.Notifications;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface INotificationService
    {
        Task<int> GetUnreadCountAsync(int userId);

        Task<(List<Notification> Items, int TotalCount,
            int TotalPages, int UnreadCount)> GetNotificationsAsync(
                int userId,
                int page,
                int pageSize,
                bool? isRead,
                string? search);

        Task<Notification?> GetByIdAsync(int userId, int id);

        Task<Notification> CreateAsync(CreateNotificationDto dto);

        Task<bool> MarkAsReadAsync(int userId, int id);

        Task<int> MarkAllAsReadAsync(int userId);

        Task<bool> DeleteAsync(int userId, int id);

        Task<int> DeleteReadAsync(int userId);
    }
}