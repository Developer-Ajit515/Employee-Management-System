using System;

namespace ERP.DOMAINMODELS.Dtos.Payment
{
    public class CreatePaymentDto
    {
        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = "Cash";

        public string? ReferenceNumber { get; set; }

        public string Status { get; set; } = "Completed";

        public string? Notes { get; set; }
    }
}