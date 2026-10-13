using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IAuditLogRepository
    {
        Task<List<AuditLog>> GetRecentAsync(int count = 10);

        Task AddAsync(AuditLog auditLog);
    }
}