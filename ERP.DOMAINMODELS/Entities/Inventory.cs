using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.DOMAINMODELS.Entities
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        // Product
        [Required]
        public int ProductId { get; set; }

        // Current stock
        [Required]
        public int Quantity { get; set; }

        // Stock movement type
        // IN, OUT, ADJUSTMENT
        [Required]
        [MaxLength(20)]
        public string TransactionType { get; set; } = string.Empty;

        // Optional reference
        // Purchase, Sale, Manual Adjustment etc.
        [MaxLength(50)]
        public string? ReferenceType { get; set; }

        public int? ReferenceId { get; set; }

        // Optional remarks
        [MaxLength(500)]
        public string? Remarks { get; set; }

        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? CreatedBy { get; set; }

        // Navigation Property
        [ForeignKey(nameof(ProductId))]
        public virtual Product? Product { get; set; }
    }
}