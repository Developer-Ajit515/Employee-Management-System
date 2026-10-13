using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    using System;
    using System.Collections.Generic;

    namespace ERP.APPLICATIONBUSINESS.DTOs.Invoice
    {
        public class InvoiceDto
        {
            public int InvoiceId { get; set; }

            public string InvoiceNumber { get; set; } = string.Empty;

            public int CustomerId { get; set; }

            public string? CustomerName { get; set; }

            public DateTime InvoiceDate { get; set; }

            public DateTime DueDate { get; set; }

            public int? SalesOrderId { get; set; }

            public string Status { get; set; } = "Pending";

            public decimal SubTotal { get; set; }

            public decimal Discount { get; set; }

            public decimal Tax { get; set; }

            public decimal TotalAmount { get; set; }

            public string? Notes { get; set; }

            public bool IsActive { get; set; }

            public List<InvoiceItemDto> InvoiceItems { get; set; }
                = new List<InvoiceItemDto>();
        }
    }
}
