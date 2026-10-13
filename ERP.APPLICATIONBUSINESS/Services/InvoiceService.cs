
using ERP.APPLICATIONBUSINESS.Interfaces;
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Dtos.ERP.APPLICATIONBUSINESS.DTOs.Invoice;
using ERP.DOMAINMODELS.Entities;
using ERP.DOMAINMODELS.Entities.ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _repository;

        public InvoiceService(IInvoiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _repository.GetAllAsync();

            return invoices.Select(MapToDto).ToList();
        }

        public async Task<InvoiceDto?> GetByIdAsync(int id)
        {
            var invoice = await _repository.GetByIdAsync(id);

            if (invoice == null)
                return null;

            return MapToDto(invoice);
        }

        public async Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = await GenerateInvoiceNumber(),

                CustomerId = dto.CustomerId,

                InvoiceDate = dto.InvoiceDate,

                DueDate = dto.DueDate,

                SalesOrderId = dto.SalesOrderId,

                Status = dto.Status,

                Discount = dto.Discount,

                Tax = dto.Tax,

                Notes = dto.Notes,

                IsActive = true,

                CreatedAt = DateTime.UtcNow
            };

            decimal subTotal = 0;

            foreach (var item in dto.InvoiceItems)
            {
                var itemSubTotal =
                    item.Quantity * item.UnitPrice;

                var itemDiscount =
                    itemSubTotal * item.Discount / 100;

                var itemTax =
                    (itemSubTotal - itemDiscount)
                    * item.Tax / 100;

                var total =
                    itemSubTotal
                    - itemDiscount
                    + itemTax;

                invoice.InvoiceItems.Add(new InvoiceItem
                {
                    ProductId = item.ProductId,

                    Quantity = item.Quantity,

                    UnitPrice = item.UnitPrice,

                    Discount = item.Discount,

                    Tax = item.Tax,

                    Total = total
                });

                subTotal += itemSubTotal;
            }

            invoice.SubTotal = subTotal;

            invoice.TotalAmount =
                subTotal
                - dto.Discount
                + dto.Tax;

            var created = await _repository.CreateAsync(invoice);

            var result = await _repository.GetByIdAsync(created.InvoiceId);

            return MapToDto(result!);
        }

        public async Task<InvoiceDto?> UpdateAsync(
            int id,
            CreateInvoiceDto dto)
        {
            var invoice = await _repository.GetByIdAsync(id);

            if (invoice == null)
                return null;

            invoice.CustomerId = dto.CustomerId;

            invoice.InvoiceDate = dto.InvoiceDate;

            invoice.DueDate = dto.DueDate;

            invoice.SalesOrderId = dto.SalesOrderId;

            invoice.Status = dto.Status;

            invoice.Discount = dto.Discount;

            invoice.Tax = dto.Tax;

            invoice.Notes = dto.Notes;

            invoice.InvoiceItems.Clear();

            decimal subTotal = 0;

            foreach (var item in dto.InvoiceItems)
            {
                var itemSubTotal =
                    item.Quantity * item.UnitPrice;

                var itemDiscount =
                    itemSubTotal * item.Discount / 100;

                var itemTax =
                    (itemSubTotal - itemDiscount)
                    * item.Tax / 100;

                var total =
                    itemSubTotal
                    - itemDiscount
                    + itemTax;

                invoice.InvoiceItems.Add(new InvoiceItem
                {
                    ProductId = item.ProductId,

                    Quantity = item.Quantity,

                    UnitPrice = item.UnitPrice,

                    Discount = item.Discount,

                    Tax = item.Tax,

                    Total = total
                });

                subTotal += itemSubTotal;
            }

            invoice.SubTotal = subTotal;

            invoice.TotalAmount =
                subTotal
                - dto.Discount
                + dto.Tax;

            await _repository.UpdateAsync(invoice);

            var updated =
                await _repository.GetByIdAsync(id);

            return MapToDto(updated!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private async Task<string> GenerateInvoiceNumber()
        {
            var invoices = await _repository.GetAllAsync();

            var nextNumber = invoices.Count + 1;

            return $"INV-{DateTime.Now:yyyy}-{nextNumber:D4}";
        }

        private InvoiceDto MapToDto(Invoice invoice)
        {
            return new InvoiceDto
            {
                InvoiceId = invoice.InvoiceId,

                InvoiceNumber = invoice.InvoiceNumber,

                CustomerId = invoice.CustomerId,

                CustomerName =
                    invoice.Customer?.Name,

                InvoiceDate = invoice.InvoiceDate,

                DueDate = invoice.DueDate,

                SalesOrderId = invoice.SalesOrderId,

                Status = invoice.Status,

                SubTotal = invoice.SubTotal,

                Discount = invoice.Discount,

                Tax = invoice.Tax,

                TotalAmount = invoice.TotalAmount,

                Notes = invoice.Notes,

                IsActive = invoice.IsActive,

                InvoiceItems = invoice.InvoiceItems
                    .Select(x => new InvoiceItemDto
                    {
                        InvoiceItemId =
                            x.InvoiceItemId,

                        ProductId =
                            x.ProductId,

                        ProductName =
                            x.Product?.Name,

                        Quantity =
                            x.Quantity,

                        UnitPrice =
                            x.UnitPrice,

                        Discount =
                            x.Discount,

                        Tax =
                            x.Tax,

                        Total =
                            x.Total
                    })
                    .ToList()
            };
        }
    }
}