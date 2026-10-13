namespace ERP.DOMAINMODELS.Entities
{
    public class SalesOrderItem
    {
        public int Id { get; set; }

        public int SalesOrderId { get; set; }

        public int ProductId { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        // Navigation
        public SalesOrder? SalesOrder { get; set; }

        public Product? Product { get; set; }
    }
}