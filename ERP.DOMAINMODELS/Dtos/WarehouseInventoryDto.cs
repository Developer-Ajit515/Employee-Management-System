using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    public class WarehouseInventoryDto { 
        public int Id { get; set; } 
        public int WarehouseId { get; set; }
        public int ProductId { get; set; } 
        public string? ProductName { get; set; } 
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; } 
        public DateTime UpdatedAt { get; set; } }
}
