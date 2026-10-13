
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class GoodsReceiptService : IGoodsReceiptService
    {
        private readonly IGoodsReceiptRepository _repository;

        private static readonly HashSet<string> AllowedStatuses =
            new(StringComparer.OrdinalIgnoreCase)
            {
                "Pending",
                "Received",
                "Partially Received",
                "Rejected"
            };

        public GoodsReceiptService(IGoodsReceiptRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GoodsReceiptDto>> GetAllAsync()
        {
            var receipts = await _repository.GetAllAsync();

            return receipts.Select(MapToDto).ToList();
        }

        public async Task<GoodsReceiptDto?> GetByIdAsync(int id)
        {
            var receipt = await _repository.GetByIdAsync(id);

            return receipt == null ? null : MapToDto(receipt);
        }

        public async Task<GoodsReceiptDto> CreateAsync(
            CreateGoodsReceiptDto dto)
        {
            Validate(dto);

            var receipt = new GoodsReceipt
            {
                ReceiptNumber = string.IsNullOrWhiteSpace(dto.ReceiptNumber)
                    ? GenerateReceiptNumber()
                    : dto.ReceiptNumber.Trim(),

                PurchaseOrderId = dto.PurchaseOrderId,
                ReceivedDate = dto.ReceivedDate,
                Status = NormalizeStatus(dto.Status),
                Notes = dto.Notes?.Trim(),

                Items = dto.Items.Select(item => new GoodsReceiptItem
                {
                    ProductName = item.ProductName.Trim(),
                    OrderedQuantity = item.OrderedQuantity,
                    ReceivedQuantity = item.ReceivedQuantity,
                    Remarks = item.Remarks?.Trim()
                }).ToList()
            };

            var created = await _repository.CreateAsync(receipt);

            return MapToDto(created);
        }

        public async Task<GoodsReceiptDto?> UpdateAsync(
            int id,
            CreateGoodsReceiptDto dto)
        {
            Validate(dto);

            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return null;

            existing.ReceiptNumber =
                string.IsNullOrWhiteSpace(dto.ReceiptNumber)
                    ? existing.ReceiptNumber
                    : dto.ReceiptNumber.Trim();

            existing.PurchaseOrderId = dto.PurchaseOrderId;
            existing.ReceivedDate = dto.ReceivedDate;
            existing.Status = NormalizeStatus(dto.Status);
            existing.Notes = dto.Notes?.Trim();

            existing.Items = dto.Items.Select(item => new GoodsReceiptItem
            {
                ProductName = item.ProductName.Trim(),
                OrderedQuantity = item.OrderedQuantity,
                ReceivedQuantity = item.ReceivedQuantity,
                Remarks = item.Remarks?.Trim()
            }).ToList();

            var updated = await _repository.UpdateAsync(existing);

            if (!updated)
                return null;

            var result = await _repository.GetByIdAsync(id);

            return result == null ? null : MapToDto(result);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static void Validate(CreateGoodsReceiptDto dto)
        {
            if (dto.PurchaseOrderId <= 0)
                throw new ArgumentException("A valid Purchase Order is required.");

            if (dto.ReceivedDate == default)
                throw new ArgumentException("Received Date is required.");

            if (string.IsNullOrWhiteSpace(dto.Status) ||
                !AllowedStatuses.Contains(dto.Status.Trim()))
            {
                throw new ArgumentException(
                    "Status must be Pending, Received, Partially Received, or Rejected."
                );
            }

            if (dto.Items == null || dto.Items.Count == 0)
                throw new ArgumentException("At least one receipt item is required.");

            foreach (var item in dto.Items)
            {
                if (string.IsNullOrWhiteSpace(item.ProductName))
                    throw new ArgumentException("Product Name is required.");

                if (item.OrderedQuantity < 0 || item.ReceivedQuantity < 0)
                    throw new ArgumentException("Quantities cannot be negative.");
            }
        }

        private static string NormalizeStatus(string status)
        {
            return AllowedStatuses.First(
                x => x.Equals(status.Trim(), StringComparison.OrdinalIgnoreCase)
            );
        }

        private static string GenerateReceiptNumber()
        {
            var suffix = Guid.NewGuid().ToString("N")[..8];

            return $"GR-{DateTime.UtcNow:yyyyMMddHHmmss}-{suffix}";
        }

        private static GoodsReceiptDto MapToDto(GoodsReceipt receipt)
        {
            return new GoodsReceiptDto
            {
                Id = receipt.Id,
                ReceiptNumber = receipt.ReceiptNumber,
                PurchaseOrderId = receipt.PurchaseOrderId,
                PurchaseOrderNumber = null,
                ReceivedDate = receipt.ReceivedDate,
                Status = receipt.Status,
                Notes = receipt.Notes,

                Items = receipt.Items.Select(item => new GoodsReceiptItemDto
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    OrderedQuantity = item.OrderedQuantity,
                    ReceivedQuantity = item.ReceivedQuantity,
                    Remarks = item.Remarks
                }).ToList()
            };
        }
    }
}