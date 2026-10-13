using AutoMapper;
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class SettingsService :
        ISettingsService
    {
        private readonly ISettingsRepository _repository;

        public SettingsService(
            ISettingsRepository repository)
        {
            _repository = repository;
        }

        public async Task<SystemSettingDto> GetAsync()
        {
            var setting = await _repository.GetAsync();

            if (setting == null)
            {
                setting = new SystemSetting
                {
                    CompanyName = "My ERP",
                    Currency = "INR",
                    TimeZone = "India Standard Time",
                    DateFormat = "dd-MM-yyyy",
                    Language = "en-IN",
                    PageSize = 10
                };

                setting =
                    await _repository.UpdateAsync(setting);
            }

            return MapToDto(setting);
        }

        public async Task<SystemSettingDto> UpdateAsync(
            UpdateSystemSettingDto dto)
        {
            var setting = await _repository.GetAsync();

            if (setting == null)
            {
                setting = new SystemSetting
                {
                    CreatedAt = DateTime.UtcNow
                };
            }

            setting.CompanyName = dto.CompanyName;
            setting.CompanyLogo = dto.CompanyLogo;
            setting.Email = dto.Email;
            setting.Phone = dto.Phone;
            setting.Address = dto.Address;
            setting.Website = dto.Website;

            setting.Currency = dto.Currency;
            setting.TimeZone = dto.TimeZone;
            setting.DateFormat = dto.DateFormat;
            setting.Language = dto.Language;
            setting.PageSize = dto.PageSize;

            setting.EmailNotifications =
                dto.EmailNotifications;

            setting.TaskNotifications =
                dto.TaskNotifications;

            setting.StockNotifications =
                dto.StockNotifications;

            setting.LowStockAlert =
                dto.LowStockAlert;

            setting.MaintenanceMode =
                dto.MaintenanceMode;
            setting.Theme = dto.Theme;

            setting.CompactSidebar = dto.CompactSidebar;

            setting.SidebarCollapsed = dto.SidebarCollapsed;

            setting.DashboardLayout = dto.DashboardLayout;

            setting.MinimumPasswordLength =
                dto.MinimumPasswordLength;

            setting.RequireUppercase =
                dto.RequireUppercase;

            setting.RequireNumber =
                dto.RequireNumber;

            setting.RequireSpecialCharacter =
                dto.RequireSpecialCharacter;

            setting.MaximumLoginAttempts =
                dto.MaximumLoginAttempts;

            setting.LockoutDurationMinutes =
                dto.LockoutDurationMinutes;

            setting.SessionTimeoutMinutes =
                dto.SessionTimeoutMinutes;

            setting.TwoFactorAuthentication =
                dto.TwoFactorAuthentication;

            setting.ForcePasswordChange =
                dto.ForcePasswordChange;



            setting.UpdatedAt = DateTime.UtcNow;

            var updated =
                await _repository.UpdateAsync(setting);

            return MapToDto(updated);
        }

        private static SystemSettingDto MapToDto(
            SystemSetting setting)
        {
            return new SystemSettingDto
            {
                Id = setting.Id,

                CompanyName = setting.CompanyName,
                CompanyLogo = setting.CompanyLogo,
                Email = setting.Email,
                Phone = setting.Phone,
                Address = setting.Address,
                Website = setting.Website,

                Currency = setting.Currency,
                TimeZone = setting.TimeZone,
                DateFormat = setting.DateFormat,
                Language = setting.Language,
                PageSize = setting.PageSize,

                EmailNotifications =
                    setting.EmailNotifications,

                TaskNotifications =
                    setting.TaskNotifications,

                StockNotifications =
                    setting.StockNotifications,

                LowStockAlert =
                    setting.LowStockAlert,

                MaintenanceMode =
                    setting.MaintenanceMode,
                    Theme = setting.Theme,

                CompactSidebar = setting.CompactSidebar,

                SidebarCollapsed = setting.SidebarCollapsed,

                DashboardLayout = setting.DashboardLayout,

                MinimumPasswordLength =
                                        setting.MinimumPasswordLength,

                RequireUppercase =
                                        setting.RequireUppercase,

                RequireNumber =
                                        setting.RequireNumber,

                RequireSpecialCharacter =
                                        setting.RequireSpecialCharacter,

                MaximumLoginAttempts =
                                        setting.MaximumLoginAttempts,

                LockoutDurationMinutes =
                                        setting.LockoutDurationMinutes,

                SessionTimeoutMinutes =
                                        setting.SessionTimeoutMinutes,

                TwoFactorAuthentication =
                                        setting.TwoFactorAuthentication,

                ForcePasswordChange =
                                        setting.ForcePasswordChange
            };
        }
    }
}