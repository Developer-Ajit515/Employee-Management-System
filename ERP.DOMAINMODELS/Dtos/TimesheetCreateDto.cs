namespace ERP.DOMAINMODELS.Dtos
{
    public class TimesheetCreateDto
    {
        public int ProjectTaskId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime WorkDate { get; set; }

        public decimal Hours { get; set; }

        public string? Description { get; set; }

        public bool IsBillable { get; set; }
    }
}