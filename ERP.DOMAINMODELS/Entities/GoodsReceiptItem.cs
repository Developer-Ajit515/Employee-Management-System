
namespace ERP.DOMAINMODELS.Entities
{
    public class GoodsReceiptItem
    {
        public int Id { get; set; }

        public int GoodsReceiptId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public decimal OrderedQuantity { get; set; }

        public decimal ReceivedQuantity { get; set; }

        public string? Remarks { get; set; }

        public GoodsReceipt? GoodsReceipt { get; set; }
    }
}