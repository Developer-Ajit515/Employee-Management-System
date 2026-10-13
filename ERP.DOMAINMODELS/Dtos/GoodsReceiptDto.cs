
using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Dtos
{
    

    public class GoodsReceiptDto
    {
        public int Id { get; set; }

        public string ReceiptNumber { get; set; } = string.Empty;

        public int PurchaseOrderId { get; set; }

        public string? PurchaseOrderNumber { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string Status { get; set; } = "Received";

        public string? Notes { get; set; }

        public List<GoodsReceiptItemDto> Items { get; set; } = new();
    }

   
}