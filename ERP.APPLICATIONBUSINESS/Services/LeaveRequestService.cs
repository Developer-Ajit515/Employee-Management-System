using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _repository;

        public LeaveRequestService(
            ILeaveRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LeaveRequestDto>> GetAllAsync()
        {
            var requests = await _repository.GetAllAsync();

            return requests.Select(MapToDto).ToList();
        }

        public async Task<LeaveRequestDto?> GetByIdAsync(int id)
        {
            var request = await _repository.GetByIdAsync(id);

            if (request == null)
                return null;

            return MapToDto(request);
        }

        public async Task<(bool Success, string Message, LeaveRequestDto? Data)>
            CreateAsync(CreateLeaveRequestDto dto)
        {
            // Date validation
            if (dto.FromDate.Date > dto.ToDate.Date)
            {
                return (
                    false,
                    "From date cannot be greater than To date.",
                    null
                );
            }

            // Employee validation
            var employeeExists =
                await _repository.EmployeeExistsAsync(dto.EmployeeId);

            if (!employeeExists)
            {
                return (
                    false,
                    "Employee not found.",
                    null
                );
            }

            // Leave Type validation
            var leaveTypeExists =
                await _repository.LeaveTypeExistsAsync(dto.LeaveTypeId);

            if (!leaveTypeExists)
            {
                return (
                    false,
                    "Leave type not found or inactive.",
                    null
                );
            }

            // Overlapping leave check
            var hasOverlap =
                await _repository.HasOverlappingLeaveAsync(
                    dto.EmployeeId,
                    dto.FromDate.Date,
                    dto.ToDate.Date);

            if (hasOverlap)
            {
                return (
                    false,
                    "Employee already has a pending or approved leave for these dates.",
                    null
                );
            }

            // Calculate total days
            int totalDays =
                (dto.ToDate.Date - dto.FromDate.Date).Days + 1;

            var leaveRequest = new LeaveRequest
            {
                EmployeeId = dto.EmployeeId,

                LeaveTypeId = dto.LeaveTypeId,

                FromDate = dto.FromDate.Date,

                ToDate = dto.ToDate.Date,

                TotalDays = totalDays,

                Reason = dto.Reason?.Trim() ?? string.Empty,

                Status = "Pending",

                AppliedDate = DateTime.UtcNow
            };

            await _repository.AddAsync(leaveRequest);

            var created =
                await _repository.GetByIdAsync(
                    leaveRequest.LeaveRequestId);

            return (
                true,
                "Leave request submitted successfully.",
                created == null ? null : MapToDto(created)
            );
        }

        public async Task<(bool Success, string Message)>
            ApproveAsync(int id, int approvedBy)
        {
            var request =
                await _repository.GetByIdAsync(id);

            if (request == null)
            {
                return (
                    false,
                    "Leave request not found."
                );
            }

            if (request.Status != "Pending")
            {
                return (
                    false,
                    $"Leave request cannot be approved because its current status is {request.Status}."
                );
            }

            request.Status = "Approved";

            request.ApprovedBy = approvedBy;

            request.ApprovedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(request);

            return (
                true,
                "Leave request approved successfully."
            );
        }

        public async Task<(bool Success, string Message)>
            RejectAsync(
                int id,
                int approvedBy,
                string? remarks)
        {
            var request =
                await _repository.GetByIdAsync(id);

            if (request == null)
            {
                return (
                    false,
                    "Leave request not found."
                );
            }

            if (request.Status != "Pending")
            {
                return (
                    false,
                    $"Leave request cannot be rejected because its current status is {request.Status}."
                );
            }

            request.Status = "Rejected";

            request.ApprovedBy = approvedBy;

            request.ApprovedDate = DateTime.UtcNow;

            request.Remarks = remarks?.Trim();

            await _repository.UpdateAsync(request);

            return (
                true,
                "Leave request rejected successfully."
            );
        }

        public async Task<(bool Success, string Message)>
            CancelAsync(int id)
        {
            var request =
                await _repository.GetByIdAsync(id);

            if (request == null)
            {
                return (
                    false,
                    "Leave request not found."
                );
            }

            if (request.Status != "Pending")
            {
                return (
                    false,
                    "Only pending leave requests can be cancelled."
                );
            }

            request.Status = "Cancelled";

            await _repository.UpdateAsync(request);

            return (
                true,
                "Leave request cancelled successfully."
            );
        }

        private static LeaveRequestDto MapToDto(
            LeaveRequest request)
        {
            return new LeaveRequestDto
            {
                LeaveRequestId = request.LeaveRequestId,

                EmployeeId = request.EmployeeId,

                EmployeeName =
                    $"{request.Employee?.FirstName} {request.Employee?.LastName}"
                    .Trim(),

                LeaveTypeId = request.LeaveTypeId,

                LeaveTypeName =
                    request.LeaveType?.Name ?? string.Empty,

                FromDate = request.FromDate,

                ToDate = request.ToDate,

                TotalDays = request.TotalDays,

                Reason = request.Reason,

                Status = request.Status,

                AppliedDate = request.AppliedDate,

                ApprovedBy = request.ApprovedBy,

                ApprovedDate = request.ApprovedDate,

                Remarks = request.Remarks
            };
        }
    }
}