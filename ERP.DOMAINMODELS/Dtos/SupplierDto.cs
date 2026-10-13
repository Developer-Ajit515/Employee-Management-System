namespace ERP.DOMAINMODELS.Dtos
{
    public class SupplierDto
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

        public bool IsActive { get; set; }
    }
}