
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IGoodsReceiptRepository
    {
        Task<List<GoodsReceipt>> GetAllAsync();

        Task<GoodsReceipt?> GetByIdAsync(int id);

        Task<GoodsReceipt> CreateAsync(GoodsReceipt receipt);

        Task<bool> UpdateAsync(GoodsReceipt receipt);

        Task<bool> DeleteAsync(int id);
    }
}