using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Dtos
{
    public class PurchaseOrderDto
    {
        public int Id { get; set; }

        public string PONumber { get; set; } = string.Empty;

        public DateTime PODate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public int SupplierId { get; set; }

        public string? SupplierCode { get; set; }

        public string? SupplierName { get; set; }

        public string Status { get; set; } = "Draft";

        public decimal SubTotal { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public decimal ShippingAmount { get; set; }

        public decimal OtherCharges { get; set; }

        public decimal GrandTotal { get; set; }

        public string? PaymentTerms { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? Notes { get; set; }

        public string? TermsAndConditions { get; set; }

        public bool IsActive { get; set; }

        public List<PurchaseOrderItemDto> Items { get; set; }
            = new();
    }
}