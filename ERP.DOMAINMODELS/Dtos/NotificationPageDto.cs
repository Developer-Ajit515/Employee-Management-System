using ERP.DOMAINMODELS.Entities;

namespace ERP.DOMAINMODELS.Dtos.Notifications
{
    public class NotificationPageDto
    {
        public List<Notification> Items { get; set; } = new();

        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public int UnreadCount { get; set; }
    }
}