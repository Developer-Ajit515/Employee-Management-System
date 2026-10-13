using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface ISettingsRepository
    {
        Task<SystemSetting?> GetAsync();

        Task<SystemSetting> UpdateAsync(
            SystemSetting setting);
    }
}