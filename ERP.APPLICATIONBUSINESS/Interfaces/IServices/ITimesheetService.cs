using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ITimesheetService
    {
        Task<List<TimesheetDto>> GetAllAsync();

        Task<TimesheetDto?> GetByIdAsync(int id);

        Task<TimesheetDto> CreateAsync(
            TimesheetCreateDto dto);

        Task<TimesheetDto?> UpdateAsync(
            int id,
            TimesheetUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}