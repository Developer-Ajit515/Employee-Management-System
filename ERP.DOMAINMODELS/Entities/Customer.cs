using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DOMAINMODELS.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }
            = string.Empty;

        public string Email { get; set; }
            = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public bool IsActive { get; set; } = true;

        

        // Navigation properties
        public ICollection<SalesOrder> SalesOrders { get; set; }
            = new List<SalesOrder>();

        public ICollection<Invoice> Invoices { get; set; }
            = new List<Invoice>();

        public ICollection<Payment> Payments { get; set; }
    = new List<Payment>();
    }
}
