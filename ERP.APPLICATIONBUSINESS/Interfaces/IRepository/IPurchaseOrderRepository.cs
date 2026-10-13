
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrder>> GetAllAsync();

        Task<PurchaseOrder?> GetByIdAsync(int id);

        Task<PurchaseOrder> CreateAsync(PurchaseOrder purchaseOrder);

        Task<bool> UpdateAsync(int id, PurchaseOrder purchaseOrder);

        Task<bool> DeleteAsync(int id);
    }
}