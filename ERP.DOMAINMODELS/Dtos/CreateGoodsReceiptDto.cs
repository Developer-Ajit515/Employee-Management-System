public class CreateGoodsReceiptDto
{
    public string? ReceiptNumber { get; set; }

    public int PurchaseOrderId { get; set; }

    public DateTime ReceivedDate { get; set; }

    public string Status { get; set; } = "Received";

    public string? Notes { get; set; }

    public List<GoodsReceiptItemDto> Items { get; set; } = new();
}