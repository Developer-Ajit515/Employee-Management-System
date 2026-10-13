using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.DOMAINMODELS.Entities
{
    [Table("Timesheets")]
    public class Timesheet
    {
        public int Id { get; set; }

        // Task against which time is logged
        public int ProjectTaskId { get; set; }

        // Employee who worked on the task
        public int EmployeeId { get; set; }

        // Date on which work was performed
        public DateTime WorkDate { get; set; }

        // Number of hours worked
        [Column(TypeName = "decimal(18,2)")]
        public decimal Hours { get; set; }

        // Work description
        public string? Description { get; set; }

        // Whether this time is billable
        public bool IsBillable { get; set; }

        // Record creation date
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ProjectTask? ProjectTask { get; set; }
    }
}