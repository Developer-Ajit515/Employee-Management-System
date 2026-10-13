using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Entities
{
    using System;

    namespace ERP.DOMAINMODELS.Entities
    {
        public class InvoiceItem
        {
            public int InvoiceItemId { get; set; }

            public int InvoiceId { get; set; }

            public int ProductId { get; set; }

            public decimal Quantity { get; set; }

            public decimal UnitPrice { get; set; }

            public decimal Discount { get; set; }

            public decimal Tax { get; set; }

            public decimal Total { get; set; }

            // Navigation
            public Invoice? Invoice { get; set; }

            public Product? Product { get; set; }
        }
    }
}