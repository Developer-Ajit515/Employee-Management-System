using ERP.APPLICATIONBUSINESS.Interfaces.IRepositories;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos.Notifications;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public Task<int> GetUnreadCountAsync(int userId)
        {
            return _repository.GetUnreadCountAsync(userId);
        }

        public async Task<(List<Notification> Items, int TotalCount,
            int TotalPages, int UnreadCount)> GetNotificationsAsync(
                int userId,
                int page,
                int pageSize,
                bool? isRead,
                string? search)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var result = await _repository.GetPagedAsync(
                userId, page, pageSize, isRead, search);

            var totalPages = (int)Math.Ceiling(
                result.TotalCount / (double)pageSize);

            return (
                result.Items,
                result.TotalCount,
                totalPages,
                result.UnreadCount
            );
        }

        public Task<Notification?> GetByIdAsync(int userId, int id)
        {
            return _repository.GetByIdAsync(userId, id);
        }

        public Task<Notification> CreateAsync(CreateNotificationDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("Title is required.");

            if (string.IsNullOrWhiteSpace(dto.Message))
                throw new ArgumentException("Message is required.");

            var notification = new Notification
            {
                UserId = dto.UserId,
                Title = dto.Title.Trim(),
                Message = dto.Message.Trim(),
                Type = string.IsNullOrWhiteSpace(dto.Type)
                    ? "Info"
                    : dto.Type.Trim(),
                ReferenceType = dto.ReferenceType?.Trim(),
                ReferenceId = dto.ReferenceId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };

            return _repository.CreateAsync(notification);
        }

        public Task<bool> MarkAsReadAsync(int userId, int id)
        {
            return _repository.MarkAsReadAsync(userId, id);
        }

        public Task<int> MarkAllAsReadAsync(int userId)
        {
            return _repository.MarkAllAsReadAsync(userId);
        }

        public Task<bool> DeleteAsync(int userId, int id)
        {
            return _repository.DeleteAsync(userId, id);
        }

        public Task<int> DeleteReadAsync(int userId)
        {
            return _repository.DeleteReadAsync(userId);
        }
    }
}