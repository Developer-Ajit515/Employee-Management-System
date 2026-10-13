using System;

namespace ERP.DOMAINMODELS.Entities
{
    public class PurchaseOrderItem
    {
        public int Id { get; set; }

        public int PurchaseOrderId { get; set; }

        public PurchaseOrder? PurchaseOrder { get; set; }

        // Product
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        // Snapshot of product information
        public string ProductName { get; set; } = string.Empty;

        public string? SKU { get; set; }

        public string? Unit { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal LineTotal { get; set; }

        public decimal ReceivedQuantity { get; set; } = 0;

        public decimal PendingQuantity { get; set; }

        public string? Remarks { get; set; }
    }
}