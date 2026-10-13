using System;

namespace ERP.DOMAINMODELS.Dtos.Payment
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }

        public string PaymentNumber { get; set; } = string.Empty;

        public int InvoiceId { get; set; }

        public string? InvoiceNumber { get; set; }

        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = "Cash";

        public string? ReferenceNumber { get; set; }

        public string Status { get; set; } = "Completed";

        public string? Notes { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public decimal InvoiceTotal { get; set; }

        public decimal PreviouslyPaid { get; set; }

        public decimal RemainingAmount { get; set; }
    }
}