using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private readonly AppDbContext _context;

        public TimesheetRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Timesheet>> GetAllAsync()
        {
            return await _context.Timesheets
                .Include(x => x.ProjectTask)
                .ThenInclude(x => x.Project)
                .AsNoTracking()
                .OrderByDescending(x => x.WorkDate)
                .ThenByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<Timesheet?> GetByIdAsync(int id)
        {
            return await _context.Timesheets
                .Include(x => x.ProjectTask)
                .ThenInclude(x => x.Project)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Timesheet> AddAsync(Timesheet timesheet)
        {
            _context.Timesheets.Add(timesheet);

            await _context.SaveChangesAsync();

            return timesheet;
        }

        public async Task<Timesheet> UpdateAsync(Timesheet timesheet)
        {
            _context.Timesheets.Update(timesheet);

            await _context.SaveChangesAsync();

            return timesheet;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var timesheet = await _context.Timesheets
                .FirstOrDefaultAsync(x => x.Id == id);

            if (timesheet == null)
                return false;

            _context.Timesheets.Remove(timesheet);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}