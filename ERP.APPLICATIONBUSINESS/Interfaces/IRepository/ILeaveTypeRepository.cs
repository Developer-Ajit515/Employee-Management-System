using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface ILeaveTypeRepository
    {
        Task<List<LeaveType>> GetAllAsync();

        Task<LeaveType?> GetByIdAsync(int id);

        Task<LeaveType> AddAsync(LeaveType leaveType);

        Task UpdateAsync(LeaveType leaveType);

        Task DeleteAsync(LeaveType leaveType);
    }
}