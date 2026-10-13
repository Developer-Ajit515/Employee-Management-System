using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    namespace ERP.APPLICATIONBUSINESS.DTOs.Invoice
    {
        public class InvoiceItemDto
        {
            public int InvoiceItemId { get; set; }

            public int ProductId { get; set; }

            public string? ProductName { get; set; }

            public decimal Quantity { get; set; }

            public decimal UnitPrice { get; set; }

            public decimal Discount { get; set; }

            public decimal Tax { get; set; }

            public decimal Total { get; set; }
        }
    }
}
