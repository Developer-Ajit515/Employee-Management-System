namespace ERP.DOMAINMODELS.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        // Make sure Tasks is never null to avoid EF errors
        public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}