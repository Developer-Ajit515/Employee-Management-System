using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface ISalesOrderRepository
    {
        Task<IEnumerable<SalesOrder>> GetAllAsync();

        Task<SalesOrder?> GetByIdAsync(int id);

        Task<SalesOrder> AddAsync(SalesOrder salesOrder);

        Task<SalesOrder?> UpdateAsync(SalesOrder salesOrder);

        Task<bool> DeleteAsync(int id);
    }
}