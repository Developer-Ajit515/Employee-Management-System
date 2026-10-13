using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly ISalesOrderRepository _repository;

        public SalesOrderService(
            ISalesOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SalesOrderDto>> GetAllAsync()
        {
            var orders = await _repository.GetAllAsync();

            return orders.Select(MapToDto);
        }

        public async Task<SalesOrderDto?> GetByIdAsync(int id)
        {
            var order = await _repository.GetByIdAsync(id);

            if (order == null)
                return null;

            return MapToDto(order);
        }

        public async Task<SalesOrderDto> CreateAsync(
    CreateSalesOrderDto dto)
        {
            var order = new SalesOrder
            {
                OrderNumber = GenerateOrderNumber(),

                CustomerId = dto.CustomerId,

                OrderDate = dto.OrderDate
                    ?? DateTime.UtcNow,

                Status = "Draft",

                DiscountAmount = dto.DiscountAmount,

                TaxAmount = dto.TaxAmount,

                Notes = dto.Notes
            };

            foreach (var itemDto in dto.Items)
            {
                var item = new SalesOrderItem
                {
                    ProductId = itemDto.ProductId,

                    Quantity = itemDto.Quantity,

                    UnitPrice = itemDto.UnitPrice,

                    DiscountAmount =
                        itemDto.DiscountAmount,

                    TaxAmount =
                        itemDto.TaxAmount
                };

                item.TotalAmount =
                    (item.Quantity * item.UnitPrice)
                    - item.DiscountAmount
                    + item.TaxAmount;

                order.Items.Add(item);
            }

            order.Subtotal = order.Items.Sum(
                x => x.Quantity * x.UnitPrice);

            order.TotalAmount =
                order.Subtotal
                - order.DiscountAmount
                + order.TaxAmount;

            var result =
                await _repository.AddAsync(order);

            // Reload with Customer + Product navigation properties
            var createdOrder =
                await _repository.GetByIdAsync(result.Id);

            return MapToDto(createdOrder!);
        }

        public async Task<SalesOrderDto?> UpdateAsync(
            int id,
            CreateSalesOrderDto dto)
        {
            var existingOrder =
                await _repository.GetByIdAsync(id);

            if (existingOrder == null)
                return null;

            var order = new SalesOrder
            {
                Id = id,

                OrderNumber =
                    existingOrder.OrderNumber,

                CustomerId = dto.CustomerId,

                OrderDate = dto.OrderDate
                    ?? existingOrder.OrderDate,

                Status = existingOrder.Status,

                DiscountAmount =
                    dto.DiscountAmount,

                TaxAmount =
                    dto.TaxAmount,

                Notes = dto.Notes
            };

            foreach (var itemDto in dto.Items)
            {
                var item = new SalesOrderItem
                {
                    ProductId = itemDto.ProductId,

                    Quantity = itemDto.Quantity,

                    UnitPrice = itemDto.UnitPrice,

                    DiscountAmount =
                        itemDto.DiscountAmount,

                    TaxAmount =
                        itemDto.TaxAmount
                };

                item.TotalAmount =
                    (item.Quantity * item.UnitPrice)
                    - item.DiscountAmount
                    + item.TaxAmount;

                order.Items.Add(item);
            }

            order.Subtotal = order.Items.Sum(
                x => x.Quantity * x.UnitPrice);

            order.TotalAmount =
                order.Subtotal
                - order.DiscountAmount
                + order.TaxAmount;

            var result =
                await _repository.UpdateAsync(order);

            if (result == null)
                return null;

            var updated =
                await _repository.GetByIdAsync(id);

            return updated == null
                ? null
                : MapToDto(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static string GenerateOrderNumber()
        {
            return $"SO-{DateTime.UtcNow:yyyyMMddHHmmssfff}";
        }

        private static SalesOrderDto MapToDto(SalesOrder order)
        {
            return new SalesOrderDto
            {
                Id = order.Id,

                OrderNumber = order.OrderNumber,

                CustomerId = order.CustomerId,

                CustomerName = order.Customer?.Name,

                OrderDate = order.OrderDate,

                Status = order.Status,

                Subtotal = order.Subtotal,

                DiscountAmount = order.DiscountAmount,

                TaxAmount = order.TaxAmount,

                TotalAmount = order.TotalAmount,

                Notes = order.Notes,

                Items = order.Items
                    .Select(item => new SalesOrderItemDto
                    {
                        Id = item.Id,

                        ProductId = item.ProductId,

                        ProductName = item.Product?.Name,

                        Quantity = item.Quantity,

                        UnitPrice = item.UnitPrice,

                        DiscountAmount = item.DiscountAmount,

                        TaxAmount = item.TaxAmount,

                        TotalAmount = item.TotalAmount
                    })
                    .ToList()
            };
        }
    }

  }
