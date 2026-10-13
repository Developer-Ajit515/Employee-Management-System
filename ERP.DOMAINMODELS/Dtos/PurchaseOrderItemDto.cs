namespace ERP.DOMAINMODELS.Dtos
{
    public class PurchaseOrderItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string? SKU { get; set; }

        public string? Unit { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal TaxPercent { get; set; }

        public decimal LineTotal { get; set; }

        public decimal ReceivedQuantity { get; set; }

        public decimal PendingQuantity { get; set; }

        public string? Remarks { get; set; }
    }
}