using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface INotificationRepository
    {
        Task<int> GetUnreadCountAsync(int userId);
    }
}