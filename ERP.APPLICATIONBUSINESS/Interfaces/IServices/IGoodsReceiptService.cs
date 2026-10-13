
using ERP.DOMAINMODELS.Dtos;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IServices
{
    public interface IGoodsReceiptService
    {
        Task<List<GoodsReceiptDto>> GetAllAsync();

        Task<GoodsReceiptDto?> GetByIdAsync(int id);

        Task<GoodsReceiptDto> CreateAsync(CreateGoodsReceiptDto dto);

        Task<GoodsReceiptDto?> UpdateAsync(
            int id,
            CreateGoodsReceiptDto dto
        );

        Task<bool> DeleteAsync(int id);
    }
}