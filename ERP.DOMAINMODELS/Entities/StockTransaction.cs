using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Entities
{
    public class StockTransaction
    {
        public long Id { get; set; }

        public int ProductId { get; set; }

        public int WarehouseId { get; set; }

        public decimal Quantity { get; set; }

        public string TransactionType { get; set; }
            = string.Empty;

        public string? ReferenceType { get; set; }

        public int? ReferenceId { get; set; }

        public DateTime TransactionDate { get; set; }
            = DateTime.UtcNow;

        public int CreatedByUserId { get; set; }
    }
}
