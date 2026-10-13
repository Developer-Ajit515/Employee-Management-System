public class GoodsReceiptItemDto
{
    public int? Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public decimal OrderedQuantity { get; set; }

    public decimal ReceivedQuantity { get; set; }

    public string? Remarks { get; set; }
}