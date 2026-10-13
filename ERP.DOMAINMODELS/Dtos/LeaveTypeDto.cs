namespace ERP.DOMAINMODELS.Dtos
{
    public class LeaveTypeDto
    {
        public int LeaveTypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DefaultDays { get; set; }

        public bool IsActive { get; set; }
    }
}   