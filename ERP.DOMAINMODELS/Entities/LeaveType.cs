namespace ERP.DOMAINMODELS.Entities
{
    public class LeaveType
    {
        public int LeaveTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DefaultDays { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
            = new List<LeaveRequest>();
    }
}