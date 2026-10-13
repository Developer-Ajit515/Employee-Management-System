using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface ITimesheetRepository
    {
        Task<List<Timesheet>> GetAllAsync();

        Task<Timesheet?> GetByIdAsync(int id);

        Task<Timesheet> AddAsync(Timesheet timesheet);

        Task<Timesheet> UpdateAsync(Timesheet timesheet);

        Task<bool> DeleteAsync(int id);
    }
}