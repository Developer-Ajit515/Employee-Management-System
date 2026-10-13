using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Dtos
{
    public class UpdateWarehouseInventoryDto { 
        public int Id { get; set; } 
        public int WarehouseId { get; set; } 
        public int ProductId { get; set; } 
        public int Quantity { get; set; } 
        public int ReorderLevel { get; set; }
    }
}
