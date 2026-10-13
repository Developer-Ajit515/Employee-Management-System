using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    public class CreateInventoryDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string TransactionType { get; set; } = string.Empty;

        public string? ReferenceType { get; set; }

        public int? ReferenceId { get; set; }

        public string? Remarks { get; set; }
    }
}
