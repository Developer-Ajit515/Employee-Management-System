
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly AppDbContext _context;

        public AttendanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Attendance>> GetAllAsync()
        {
            return await _context.Attendances
                .Include(x => x.Employee)
                .AsNoTracking()
                .OrderByDescending(x => x.AttendanceDate)
                .ThenBy(x => x.Employee!.FirstName)
                .ToListAsync();
        }

        public async Task<Attendance?> GetByIdAsync(int id)
        {
            return await _context.Attendances
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x =>
                    x.AttendanceId == id);
        }

        public async Task<Attendance?> GetByEmployeeAndDateAsync(
            int employeeId,
            DateTime attendanceDate)
        {
            var date = attendanceDate.Date;

            return await _context.Attendances
                .FirstOrDefaultAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.AttendanceDate.Date == date);
        }

        
public async Task<Attendance> AddAsync(Attendance attendance)
        {
            await _context.Attendances.AddAsync(attendance);

            await _context.SaveChangesAsync();

            await _context.Entry(attendance)
                .Reference(x => x.Employee)
                .LoadAsync();

            return attendance;
        }


        public async Task UpdateAsync(
            Attendance attendance)
        {
            _context.Attendances.Update(attendance);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(
            Attendance attendance)
        {
            _context.Attendances.Remove(attendance);

            await _context.SaveChangesAsync();
        }
    }
}

