using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }

        public string PONumber { get; set; } = string.Empty;

        public DateTime PODate { get; set; } = DateTime.UtcNow;

        public DateTime? ExpectedDeliveryDate { get; set; }

        // Supplier
        public int SupplierId { get; set; }

        public Supplier? Supplier { get; set; }

        // Status
        public string Status { get; set; } = "Pending";

        // Financial
        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal ShippingAmount { get; set; }

        public decimal OtherCharges { get; set; }

        public decimal GrandTotal { get; set; }

        // Additional information
        public string? PaymentTerms { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? Notes { get; set; }

        public string? TermsAndConditions { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }

        public ICollection<PurchaseOrderItem> Items { get; set; }
            = new List<PurchaseOrderItem>();
    }
}