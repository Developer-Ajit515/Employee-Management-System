
using System;
using System.Collections.Generic;

namespace ERP.DOMAINMODELS.Entities
{
    public class GoodsReceipt
    {
        public int Id { get; set; }

        public string ReceiptNumber { get; set; } = string.Empty;

        public int PurchaseOrderId { get; set; }

        public DateTime ReceivedDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = "Received";

        public string? Notes { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<GoodsReceiptItem> Items { get; set; }
            = new List<GoodsReceiptItem>();
    }
}