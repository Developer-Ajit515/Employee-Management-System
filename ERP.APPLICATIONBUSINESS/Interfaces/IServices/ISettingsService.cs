using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ISettingsService
    {
        Task<SystemSettingDto> GetAsync();

        Task<SystemSettingDto> UpdateAsync(
            UpdateSystemSettingDto dto);
    }
}