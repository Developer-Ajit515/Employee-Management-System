using ERP.DOMAINMODELS.Dtos.ERP.APPLICATIONBUSINESS.DTOs.Invoice;

namespace ERP.APPLICATIONBUSINESS.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDto>> GetAllAsync();

        Task<InvoiceDto?> GetByIdAsync(int id);

        Task<InvoiceDto> CreateAsync(CreateInvoiceDto dto);

        Task<InvoiceDto?> UpdateAsync(int id, CreateInvoiceDto dto);

        Task<bool> DeleteAsync(int id);
    }
}