using ERP.DOMAINMODELS.Entities.ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public int? SalesOrderId { get; set; }

        public string Status { get; set; } = "Pending";

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Customer? Customer { get; set; }

        public ICollection<InvoiceItem> InvoiceItems { get; set; }
            = new List<InvoiceItem>();

        public ICollection<Payment> Payments { get; set; }
    = new List<Payment>();
    }
}