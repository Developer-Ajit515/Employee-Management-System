using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class TimesheetService : ITimesheetService
    {
        private readonly ITimesheetRepository _repository;

        public TimesheetService(
            ITimesheetRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TimesheetDto>> GetAllAsync()
        {
            var timesheets =
                await _repository.GetAllAsync();

            return timesheets.Select(x => new TimesheetDto
            {
                Id = x.Id,

                ProjectTaskId = x.ProjectTaskId,

                EmployeeId = x.EmployeeId,

                WorkDate = x.WorkDate,

                Hours = x.Hours,

                Description = x.Description,

                IsBillable = x.IsBillable,

                CreatedAt = x.CreatedAt,

                TaskCode = x.ProjectTask?.TaskCode,

                TaskTitle = x.ProjectTask?.Title,

                ProjectName =
                    x.ProjectTask?.Project?.Name

            }).ToList();
        }

        public async Task<TimesheetDto?> GetByIdAsync(int id)
        {
            var x = await _repository.GetByIdAsync(id);

            if (x == null)
                return null;

            return new TimesheetDto
            {
                Id = x.Id,

                ProjectTaskId = x.ProjectTaskId,

                EmployeeId = x.EmployeeId,

                WorkDate = x.WorkDate,

                Hours = x.Hours,

                Description = x.Description,

                IsBillable = x.IsBillable,

                CreatedAt = x.CreatedAt,

                TaskCode = x.ProjectTask?.TaskCode,

                TaskTitle = x.ProjectTask?.Title,

                ProjectName =
                    x.ProjectTask?.Project?.Name
            };
        }

        public async Task<TimesheetDto> CreateAsync(
            TimesheetCreateDto dto)
        {
            if (dto.Hours <= 0)
                throw new ArgumentException(
                    "Hours must be greater than zero.");

            if (dto.Hours > 24)
                throw new ArgumentException(
                    "Hours cannot be greater than 24.");

            var entity = new Timesheet
            {
                ProjectTaskId = dto.ProjectTaskId,

                EmployeeId = dto.EmployeeId,

                WorkDate = dto.WorkDate,

                Hours = dto.Hours,

                Description = dto.Description,

                IsBillable = dto.IsBillable,

                CreatedAt = DateTime.UtcNow
            };

            var result =
                await _repository.AddAsync(entity);

            return await GetByIdAsync(result.Id)
                ?? throw new Exception(
                    "Unable to create timesheet.");
        }

        public async Task<TimesheetDto?> UpdateAsync(
            int id,
            TimesheetUpdateDto dto)
        {
            var entity =
                await _repository.GetByIdAsync(id);

            if (entity == null)
                return null;

            if (dto.Hours <= 0)
                throw new ArgumentException(
                    "Hours must be greater than zero.");

            if (dto.Hours > 24)
                throw new ArgumentException(
                    "Hours cannot be greater than 24.");

            entity.ProjectTaskId = dto.ProjectTaskId;

            entity.EmployeeId = dto.EmployeeId;

            entity.WorkDate = dto.WorkDate;

            entity.Hours = dto.Hours;

            entity.Description = dto.Description;

            entity.IsBillable = dto.IsBillable;

            await _repository.UpdateAsync(entity);

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}