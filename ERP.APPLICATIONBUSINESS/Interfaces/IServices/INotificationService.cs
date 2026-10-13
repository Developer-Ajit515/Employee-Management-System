namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface INotificationService
    {
        Task<int> GetUnreadCountAsync(int userId);
    }
}