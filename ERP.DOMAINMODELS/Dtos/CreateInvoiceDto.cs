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
        public class CreateInvoiceDto
        {
            public int CustomerId { get; set; }

            public DateTime InvoiceDate { get; set; }

            public DateTime DueDate { get; set; }

            public int? SalesOrderId { get; set; }

            public string Status { get; set; } = "Pending";

            public decimal Discount { get; set; }

            public decimal Tax { get; set; }

            public string? Notes { get; set; }

            public List<CreateInvoiceItemDto> InvoiceItems { get; set; }
                = new List<CreateInvoiceItemDto>();
        }
    }
}
