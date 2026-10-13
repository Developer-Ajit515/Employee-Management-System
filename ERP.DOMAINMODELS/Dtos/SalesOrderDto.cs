using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Dtos
{
    public class SalesOrderDto
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public decimal Subtotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Notes { get; set; }

        public List<SalesOrderItemDto> Items { get; set; }
            = new List<SalesOrderItemDto>();
    }
}