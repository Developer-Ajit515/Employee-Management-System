using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    namespace ERP.APPLICATIONBUSINESS.DTOs.Invoice
    {
        public class CreateInvoiceItemDto
        {
            public int ProductId { get; set; }

            public decimal Quantity { get; set; }

            public decimal UnitPrice { get; set; }

            public decimal Discount { get; set; }

            public decimal Tax { get; set; }
        }
    }
}
