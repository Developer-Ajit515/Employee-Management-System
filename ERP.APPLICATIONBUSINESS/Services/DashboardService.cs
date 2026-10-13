using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _repository;
    private readonly IAuditLogService _auditLogService;

    public DashboardService(
        IDashboardRepository repository,
        IAuditLogService auditLogService)
    {
        _repository = repository;
        _auditLogService = auditLogService;
    }

    public async Task<DashboardDto> GetDashboardDataAsync()
    {
        var dashboard =
            await _repository.GetDashboardDataAsync();

        var logs =
            await _auditLogService.GetRecentAsync(10);

        dashboard.RecentActivities = logs
            .Select(x => new RecentActivityDto
            {
                Type = x.EntityName,
                Title = x.Action,
                Description = x.Description,
                CreatedAt = x.CreatedAt
            })
            .ToList();

        return dashboard;
    }
}