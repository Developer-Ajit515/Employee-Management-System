using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface ILeaveRequestRepository
    {
        Task<List<LeaveRequest>> GetAllAsync();

        Task<LeaveRequest?> GetByIdAsync(int id);

        Task<bool> EmployeeExistsAsync(int employeeId);

        Task<bool> LeaveTypeExistsAsync(int leaveTypeId);

        Task<bool> HasOverlappingLeaveAsync(
            int employeeId,
            DateTime fromDate,
            DateTime toDate);

        Task<LeaveRequest> AddAsync(LeaveRequest leaveRequest);

        Task UpdateAsync(LeaveRequest leaveRequest);
    }
}