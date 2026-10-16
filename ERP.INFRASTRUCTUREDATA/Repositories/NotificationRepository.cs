using ERP.APPLICATIONBUSINESS.Interfaces.IRepositories;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetUnreadCountAsync(int userId)
        {
            return await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);
        }

        public async Task<(List<Notification> Items, int TotalCount, int UnreadCount)>
            GetPagedAsync(
                int userId,
                int page,
                int pageSize,
                bool? isRead,
                string? search)
        {
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var query = _context.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == userId);

            if (isRead.HasValue)
                query = query.Where(n => n.IsRead == isRead.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();

                query = query.Where(n =>
                    n.Title.Contains(term) ||
                    n.Message.Contains(term) ||
                    n.Type.Contains(term));
            }

            var totalCount = await query.CountAsync();

            var unreadCount = await _context.Notifications
                .CountAsync(n => n.UserId == userId && !n.IsRead);

            var items = await query
                .OrderByDescending(n => n.CreatedAt)
                .ThenByDescending(n => n.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount, unreadCount);
        }

        public async Task<Notification?> GetByIdAsync(int userId, int id)
        {
            return await _context.Notifications
                .AsNoTracking()
                .FirstOrDefaultAsync(n =>
                    n.Id == id && n.UserId == userId);
        }

        public async Task<Notification> CreateAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return notification;
        }

        public async Task<bool> MarkAsReadAsync(int userId, int id)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n =>
                    n.Id == id && n.UserId == userId);

            if (notification == null)
                return false;

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<int> MarkAllAsReadAsync(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
                notification.IsRead = true;

            if (notifications.Count > 0)
                await _context.SaveChangesAsync();

            return notifications.Count;
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n =>
                    n.Id == id && n.UserId == userId);

            if (notification == null)
                return false;

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> DeleteReadAsync(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && n.IsRead)
                .ToListAsync();

            if (notifications.Count > 0)
            {
                _context.Notifications.RemoveRange(notifications);
                await _context.SaveChangesAsync();
            }

            return notifications.Count;
        }
    }
}