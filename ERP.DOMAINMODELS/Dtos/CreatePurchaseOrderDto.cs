using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Dtos
{
    public class CreatePurchaseOrderDto
    {
        public DateTime PODate { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public int SupplierId { get; set; }

        public string? PaymentTerms { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? Notes { get; set; }

        public string? TermsAndConditions { get; set; }

        public decimal ShippingAmount { get; set; }

        public decimal OtherCharges { get; set; }

        public List<CreatePurchaseOrderItemDto> Items { get; set; }
            = new();
    }
}