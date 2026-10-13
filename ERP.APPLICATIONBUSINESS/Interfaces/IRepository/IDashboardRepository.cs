using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IDashboardRepository
    {
        Task<DashboardDto> GetDashboardDataAsync();
    }
}