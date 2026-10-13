using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Entities
{
    public class SalesOrder
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Draft";

        public decimal Subtotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public Customer? Customer { get; set; }



        // Order items
        public ICollection<SalesOrderItem> Items { get; set; }
            = new List<SalesOrderItem>();

     
    }
}