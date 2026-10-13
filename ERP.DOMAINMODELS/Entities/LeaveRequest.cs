namespace ERP.DOMAINMODELS.Entities
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation Property
        public Employee? Employee { get; set; }

        // Foreign Key
        public int LeaveTypeId { get; set; }

        // Navigation Property
        public LeaveType? LeaveType { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int TotalDays { get; set; }

        public string Reason { get; set; } = string.Empty;

        // Pending / Approved / Rejected / Cancelled
        public string Status { get; set; } = "Pending";

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public string? Remarks { get; set; }
    }
}