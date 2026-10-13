using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly IAuditLogRepository _repository;

        public AuditLogService(
            IAuditLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AuditLog>> GetRecentAsync(
            int count = 10)
        {
            return await _repository.GetRecentAsync(count);
        }

        public async Task AddAsync(AuditLog auditLog)
        {
            await _repository.AddAsync(auditLog);
        }
    }
}