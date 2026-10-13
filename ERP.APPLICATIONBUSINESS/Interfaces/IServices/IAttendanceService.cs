
using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IAttendanceService
    {
        Task<List<AttendanceResponseDto>> GetAllAsync();

        Task<AttendanceResponseDto?> GetByIdAsync(int id);

        Task<AttendanceResponseDto> CreateAsync(
            AttendanceCreateDto dto);

        Task<AttendanceResponseDto?> UpdateAsync(
            int id,
            AttendanceUpdateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}

