using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    public class CreateStockInDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string? Remarks { get; set; }
    }
}
