using System;

namespace ERP.DOMAINMODELS.Entities
{
    public class Supplier
    {
        public int Id { get; set; }

        public string SupplierCode { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string? ContactPerson { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? GSTNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Country { get; set; }

        public string? PostalCode { get; set; }

        public string? PaymentTerms { get; set; }

        public string? BankName { get; set; }

        public string? BankAccountNumber { get; set; }

        public string? IFSCCode { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedDate { get; set; }


        // Navigation

        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
            = new List<PurchaseOrder>();
    }
}