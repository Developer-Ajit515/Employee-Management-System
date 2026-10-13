using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly AppDbContext _context;

        public LeaveRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LeaveRequest>> GetAllAsync()
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .AsNoTracking()
                .OrderByDescending(x => x.AppliedDate)
                .ToListAsync();
        }

        public async Task<LeaveRequest?> GetByIdAsync(int id)
        {
            return await _context.LeaveRequests
                .Include(x => x.Employee)
                .Include(x => x.LeaveType)
                .FirstOrDefaultAsync(x => x.LeaveRequestId == id);
        }

        public async Task<bool> EmployeeExistsAsync(int employeeId)
        {
            return await _context.Employees
                .AnyAsync(x => x.EmployeeId == employeeId);
        }

        public async Task<bool> LeaveTypeExistsAsync(int leaveTypeId)
        {
            return await _context.LeaveTypes
                .AnyAsync(x =>
                    x.LeaveTypeId == leaveTypeId &&
                    x.IsActive);
        }

        public async Task<bool> HasOverlappingLeaveAsync(
            int employeeId,
            DateTime fromDate,
            DateTime toDate)
        {
            return await _context.LeaveRequests
                .AnyAsync(x =>
                    x.EmployeeId == employeeId &&
                    (x.Status == "Pending" ||
                     x.Status == "Approved") &&
                    fromDate <= x.ToDate &&
                    toDate >= x.FromDate);
        }

        public async Task<LeaveRequest> AddAsync(
            LeaveRequest leaveRequest)
        {
            await _context.LeaveRequests.AddAsync(leaveRequest);

            await _context.SaveChangesAsync();

            return leaveRequest;
        }

        public async Task UpdateAsync(
            LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);

            await _context.SaveChangesAsync();
        }
    }
}