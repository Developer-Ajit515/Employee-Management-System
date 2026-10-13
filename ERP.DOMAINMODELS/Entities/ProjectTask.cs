namespace ERP.DOMAINMODELS.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }

        public string TaskCode { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int ProjectId { get; set; }

        public int? AssignedToEmployeeId { get; set; }

        public string Priority { get; set; }
            = "Medium";

        public string Status { get; set; }
            = "Pending";

        public DateTime? DueDate { get; set; }

        public decimal EstimatedHours { get; set; }

        public decimal ActualHours { get; set; }

        public DateTime? CompletedAt { get; set; }

        public bool IsCompleted { get; set; }

        public Project? Project { get; set; }

        public ICollection<Timesheet> Timesheets { get; set; }
            = new List<Timesheet>();
    }
}