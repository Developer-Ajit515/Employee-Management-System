using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class SettingsRepository :
        ISettingsRepository
    {
        private readonly AppDbContext _context;

        public SettingsRepository(
            AppDbContext context)
        {
            _context = context;
        }

        public async Task<SystemSetting?> GetAsync()
        {
            return await _context.SystemSettings
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<SystemSetting> UpdateAsync(
            SystemSetting setting)

        {
            _context.SystemSettings.Update(setting);

            await _context.SaveChangesAsync();

            return setting;
        }
    }
}