namespace ERP.DOMAINMODELS.Dtos
{
    public class TimesheetDto
    {
        public int Id { get; set; }

        public int ProjectTaskId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime WorkDate { get; set; }

        public decimal Hours { get; set; }

        public string? Description { get; set; }

        public bool IsBillable { get; set; }

        public DateTime CreatedAt { get; set; }

        // Display fields
        public string? TaskCode { get; set; }

        public string? TaskTitle { get; set; }

        public string? ProjectName { get; set; }
    }
}