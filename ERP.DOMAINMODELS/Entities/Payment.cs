using System;

namespace ERP.DOMAINMODELS.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public string PaymentNumber { get; set; } = string.Empty;

        public int InvoiceId { get; set; }

        public int CustomerId { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = "Cash";

        public string? ReferenceNumber { get; set; }

        public string Status { get; set; } = "Completed";

        public string? Notes { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public string? PaymentGateway { get; set; }

        public string? TransactionId { get; set; }

        public string? GatewayOrderId { get; set; }

        public string? GatewayPaymentId { get; set; }

        public string? GatewayResponse { get; set; }

        // Navigation properties
        public Invoice? Invoice { get; set; }

        public Customer? Customer { get; set; }
    }
}