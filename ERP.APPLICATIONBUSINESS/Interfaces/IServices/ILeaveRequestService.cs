using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface ILeaveRequestService
    {
        Task<List<LeaveRequestDto>> GetAllAsync();

        Task<LeaveRequestDto?> GetByIdAsync(int id);

        Task<(bool Success, string Message, LeaveRequestDto? Data)>
            CreateAsync(CreateLeaveRequestDto dto);

        Task<(bool Success, string Message)>
            ApproveAsync(int id, int approvedBy);

        Task<(bool Success, string Message)>
            RejectAsync(int id, int approvedBy, string? remarks);

        Task<(bool Success, string Message)>
            CancelAsync(int id);
    }
}