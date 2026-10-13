namespace ERP.DOMAINMODELS.Dtos
{
    public class CreateLeaveTypeDto
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int DefaultDays { get; set; }
    }
}