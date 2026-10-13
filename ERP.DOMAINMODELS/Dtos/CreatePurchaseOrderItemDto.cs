namespace ERP.DOMAINMODELS.Dtos
{
    public class CreatePurchaseOrderItemDto
    {
        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountPercent { get; set; }

        public decimal TaxPercent { get; set; }

        public string? Remarks { get; set; }
    }
}