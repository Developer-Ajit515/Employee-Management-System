using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IAuditLogService
    {
        Task<List<AuditLog>> GetRecentAsync(int count = 10);

        Task AddAsync(AuditLog auditLog);
    }
}