using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _repository;

        public LeaveTypeService(ILeaveTypeRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LeaveTypeDto>> GetAllAsync()
        {
            var leaveTypes = await _repository.GetAllAsync();

            return leaveTypes.Select(x => new LeaveTypeDto
            {
                LeaveTypeId = x.LeaveTypeId,
                Name = x.Name,
                Description = x.Description,
                DefaultDays = x.DefaultDays,
                IsActive = x.IsActive
            }).ToList();
        }

        public async Task<LeaveTypeDto?> GetByIdAsync(int id)
        {
            var leaveType = await _repository.GetByIdAsync(id);

            if (leaveType == null)
                return null;

            return new LeaveTypeDto
            {
                LeaveTypeId = leaveType.LeaveTypeId,
                Name = leaveType.Name,
                Description = leaveType.Description,
                DefaultDays = leaveType.DefaultDays,
                IsActive = leaveType.IsActive
            };
        }

        public async Task<LeaveTypeDto> CreateAsync(CreateLeaveTypeDto dto)
        {
            var leaveType = new LeaveType
            {
                Name = dto.Name,
                Description = dto.Description,
                DefaultDays = dto.DefaultDays,
                IsActive = true
            };

            await _repository.AddAsync(leaveType);

            return new LeaveTypeDto
            {
                LeaveTypeId = leaveType.LeaveTypeId,
                Name = leaveType.Name,
                Description = leaveType.Description,
                DefaultDays = leaveType.DefaultDays,
                IsActive = leaveType.IsActive
            };
        }

        public async Task<bool> UpdateAsync(int id, CreateLeaveTypeDto dto)
        {
            var leaveType = await _repository.GetByIdAsync(id);

            if (leaveType == null)
                return false;

            leaveType.Name = dto.Name;
            leaveType.Description = dto.Description;
            leaveType.DefaultDays = dto.DefaultDays;

            await _repository.UpdateAsync(leaveType);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var leaveType = await _repository.GetByIdAsync(id);

            if (leaveType == null)
                return false;

            await _repository.DeleteAsync(leaveType);

            return true;
        }
    }
}