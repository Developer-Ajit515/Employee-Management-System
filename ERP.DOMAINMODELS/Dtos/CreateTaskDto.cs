using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.DOMAINMODELS.Dtos.Tasks
{
    public class CreateTaskDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(2000)]
        public string? Description { get; set; }

        [Range(1, int.MaxValue)]
        public int ProjectId { get; set; }

        [Range(1, int.MaxValue)]
        public int? AssignedToEmployeeId { get; set; }

        [RegularExpression(
            "^(Low|Medium|High)$",
            ErrorMessage = "Priority must be Low, Medium, or High.")]
        public string Priority { get; set; } = "Medium";

        public DateTime? DueDate { get; set; }

        [Range(0, 100000)]
        public decimal EstimatedHours { get; set; }
    }
}