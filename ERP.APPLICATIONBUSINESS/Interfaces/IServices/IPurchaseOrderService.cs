
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrder>> GetAllAsync();

        Task<PurchaseOrder?> GetByIdAsync(int id);

        Task<PurchaseOrder> CreateAsync(PurchaseOrder purchaseOrder);

        Task<bool> UpdateAsync(int id, PurchaseOrder purchaseOrder);

        Task<bool> DeleteAsync(int id);
    }
}