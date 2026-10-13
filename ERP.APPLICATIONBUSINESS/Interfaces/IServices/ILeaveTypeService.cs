using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ILeaveTypeService
    {
        Task<List<LeaveTypeDto>> GetAllAsync();

        Task<LeaveTypeDto?> GetByIdAsync(int id);

        Task<LeaveTypeDto> CreateAsync(CreateLeaveTypeDto dto);

        Task<bool> UpdateAsync(int id, CreateLeaveTypeDto dto);

        Task<bool> DeleteAsync(int id);
    }
}