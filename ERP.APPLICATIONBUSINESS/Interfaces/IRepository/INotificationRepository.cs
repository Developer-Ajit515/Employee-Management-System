using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepositories
{
    public interface INotificationRepository
    {
        Task<int> GetUnreadCountAsync(int userId);

        Task<(List<Notification> Items, int TotalCount, int UnreadCount)>
            GetPagedAsync(
                int userId,
                int page,
                int pageSize,
                bool? isRead,
                string? search);

        Task<Notification?> GetByIdAsync(int userId, int id);

        Task<Notification> CreateAsync(Notification notification);

        Task<bool> MarkAsReadAsync(int userId, int id);

        Task<int> MarkAllAsReadAsync(int userId);

        Task<bool> DeleteAsync(int userId, int id);

        Task<int> DeleteReadAsync(int userId);
    }
}