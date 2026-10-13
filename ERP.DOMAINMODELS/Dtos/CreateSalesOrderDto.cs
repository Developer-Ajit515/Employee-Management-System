using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Dtos
{
    public class CreateSalesOrderDto
    {
        public int CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public string? Notes { get; set; }

        public List<CreateSalesOrderItemDto> Items { get; set; }
            = new List<CreateSalesOrderItemDto>();
    }

   
}