
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository _repository;

        public AttendanceService(
            IAttendanceRepository repository)
        {
            _repository = repository;
        }

        // ==========================================
        // GET ALL
        // ==========================================

        public async Task<List<AttendanceResponseDto>> GetAllAsync()
        {
            var attendance = await _repository.GetAllAsync();

            return attendance
                .Select(MapToDto)
                .ToList();
        }

        // ==========================================
        // GET BY ID
        // ==========================================

        public async Task<AttendanceResponseDto?> GetByIdAsync(int id)
        {
            var attendance = await _repository.GetByIdAsync(id);

            if (attendance == null)
                return null;

            return MapToDto(attendance);
        }

        // ==========================================
        // CREATE
        // ==========================================

        public async Task<AttendanceResponseDto> CreateAsync(
            AttendanceCreateDto dto)
        {
            // ------------------------------------------
            // Validate Employee
            // ------------------------------------------

            if (dto.EmployeeId <= 0)
            {
                throw new Exception(
                    "Please select a valid employee.");
            }

            // ------------------------------------------
            // Validate Attendance Date
            // ------------------------------------------

            if (dto.AttendanceDate == default)
            {
                throw new Exception(
                    "Attendance date is required.");
            }

            // ------------------------------------------
            // Check Duplicate Attendance
            // ------------------------------------------

            var existing =
                await _repository.GetByEmployeeAndDateAsync(
                    dto.EmployeeId,
                    dto.AttendanceDate);

            if (existing != null)
            {
                throw new Exception(
                    $"Attendance already exists for Employee ID {dto.EmployeeId} on {dto.AttendanceDate:yyyy-MM-dd}.");
            }

            // ------------------------------------------
            // Validate Status
            // ------------------------------------------

            var allowedStatuses = new[]
            {
                "Present",
                "Absent",
                "Late",
                "HalfDay",
                "Leave"
            };

            if (!allowedStatuses.Contains(
                    dto.Status,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception(
                    $"Invalid attendance status: {dto.Status}");
            }

            // ------------------------------------------
            // Convert Check-In / Check-Out Time
            // ------------------------------------------

            TimeSpan? checkInTime = null;
            TimeSpan? checkOutTime = null;

            if (!string.IsNullOrWhiteSpace(dto.CheckInTime))
            {
                if (!TimeSpan.TryParse(
                        dto.CheckInTime,
                        out var parsedCheckIn))
                {
                    throw new Exception(
                        "Invalid check-in time.");
                }

                checkInTime = parsedCheckIn;
            }

            if (!string.IsNullOrWhiteSpace(dto.CheckOutTime))
            {
                if (!TimeSpan.TryParse(
                        dto.CheckOutTime,
                        out var parsedCheckOut))
                {
                    throw new Exception(
                        "Invalid check-out time.");
                }

                checkOutTime = parsedCheckOut;
            }

            // ------------------------------------------
            // Validate Time
            // ------------------------------------------

            if (checkInTime.HasValue &&
                checkOutTime.HasValue &&
                checkOutTime.Value < checkInTime.Value)
            {
                throw new Exception(
                    "Check-out time cannot be earlier than check-in time.");
            }

            // ------------------------------------------
            // Create Entity
            // ------------------------------------------

            var attendance = new Attendance
            {
                EmployeeId = dto.EmployeeId,

                AttendanceDate =
                    dto.AttendanceDate.Date,

                CheckInTime =
                    checkInTime,

                CheckOutTime =
                    checkOutTime,

                Status =
                    dto.Status,

                Remarks =
                    string.IsNullOrWhiteSpace(dto.Remarks)
                        ? null
                        : dto.Remarks.Trim(),

                CreatedDate =
                    DateTime.UtcNow
            };

            // ------------------------------------------
            // Save
            // ------------------------------------------

            var created =
                await _repository.AddAsync(attendance);

            // ------------------------------------------
            // Return DTO
            // ------------------------------------------

            return MapToDto(created);
        }

        // ==========================================
        // UPDATE
        // ==========================================

        public async Task<AttendanceResponseDto?> UpdateAsync(
            int id,
            AttendanceUpdateDto dto)
        {
            // ------------------------------------------
            // Find Attendance
            // ------------------------------------------

            var attendance =
                await _repository.GetByIdAsync(id);

            if (attendance == null)
                return null;

            // ------------------------------------------
            // Validate Employee
            // ------------------------------------------

            if (dto.EmployeeId <= 0)
            {
                throw new Exception(
                    "Please select a valid employee.");
            }

            // ------------------------------------------
            // Validate Attendance Date
            // ------------------------------------------

            if (dto.AttendanceDate == default)
            {
                throw new Exception(
                    "Attendance date is required.");
            }

            // ------------------------------------------
            // Validate Status
            // ------------------------------------------

            var validStatuses = new[]
            {
                "Present",
                "Absent",
                "Late",
                "HalfDay",
                "Leave"
            };

            if (!validStatuses.Contains(
                    dto.Status,
                    StringComparer.OrdinalIgnoreCase))
            {
                throw new Exception(
                    $"Invalid attendance status: {dto.Status}");
            }

            // ------------------------------------------
            // Check Duplicate Attendance
            // ------------------------------------------

            var existing =
                await _repository.GetByEmployeeAndDateAsync(
                    dto.EmployeeId,
                    dto.AttendanceDate);

            if (existing != null &&
                existing.AttendanceId != id)
            {
                throw new Exception(
                    "Attendance already exists for this employee on this date.");
            }

            // ------------------------------------------
            // Convert Check-In / Check-Out Time
            // ------------------------------------------

            TimeSpan? checkInTime = null;
            TimeSpan? checkOutTime = null;

            if (!string.IsNullOrWhiteSpace(dto.CheckInTime))
            {
                if (!TimeSpan.TryParse(
                        dto.CheckInTime,
                        out var parsedCheckIn))
                {
                    throw new Exception(
                        "Invalid check-in time.");
                }

                checkInTime = parsedCheckIn;
            }

            if (!string.IsNullOrWhiteSpace(dto.CheckOutTime))
            {
                if (!TimeSpan.TryParse(
                        dto.CheckOutTime,
                        out var parsedCheckOut))
                {
                    throw new Exception(
                        "Invalid check-out time.");
                }

                checkOutTime = parsedCheckOut;
            }

            // ------------------------------------------
            // Validate Time
            // ------------------------------------------

            if (checkInTime.HasValue &&
                checkOutTime.HasValue &&
                checkOutTime.Value < checkInTime.Value)
            {
                throw new Exception(
                    "Check-out time cannot be earlier than check-in time.");
            }

            // ------------------------------------------
            // Update Entity
            // ------------------------------------------

            attendance.EmployeeId =
                dto.EmployeeId;

            attendance.AttendanceDate =
                dto.AttendanceDate.Date;

            attendance.CheckInTime =
                checkInTime;

            attendance.CheckOutTime =
                checkOutTime;

            attendance.Status =
                dto.Status;

            attendance.Remarks =
                string.IsNullOrWhiteSpace(dto.Remarks)
                    ? null
                    : dto.Remarks.Trim();

            attendance.UpdatedDate =
                DateTime.UtcNow;

            // ------------------------------------------
            // Save
            // ------------------------------------------

            await _repository.UpdateAsync(
                attendance);

            // ------------------------------------------
            // Get Updated Record
            // ------------------------------------------

            var result =
                await _repository.GetByIdAsync(id);

            if (result == null)
                return null;

            return MapToDto(result);
        }

        // ==========================================
        // DELETE
        // ==========================================

        public async Task<bool> DeleteAsync(int id)
        {
            var attendance =
                await _repository.GetByIdAsync(id);

            if (attendance == null)
                return false;

            await _repository.DeleteAsync(
                attendance);

            return true;
        }

        // ==========================================
        // MAPPER
        // ==========================================

        private static AttendanceResponseDto MapToDto(
            Attendance attendance)
        {
            return new AttendanceResponseDto
            {
                AttendanceId =
                    attendance.AttendanceId,

                EmployeeId =
                    attendance.EmployeeId,

                EmployeeName =
                    attendance.Employee == null
                        ? string.Empty
                        : $"{attendance.Employee.FirstName} {attendance.Employee.LastName}",

                AttendanceDate =
                    attendance.AttendanceDate,

                CheckInTime =
                    attendance.CheckInTime,

                CheckOutTime =
                    attendance.CheckOutTime,

                Status =
                    attendance.Status,

                Remarks =
                    attendance.Remarks,

                CreatedDate =
                    attendance.CreatedDate,

                UpdatedDate =
                    attendance.UpdatedDate
            };
        }
    }
}

