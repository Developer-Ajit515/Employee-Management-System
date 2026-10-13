
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
// Adjust this using statement to your actual AppDbContext namespace.
using ERP.INFRASTRUCTUREDATA;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class GoodsReceiptRepository : IGoodsReceiptRepository
    {
        private readonly AppDbContext _context;

        public GoodsReceiptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<GoodsReceipt>> GetAllAsync()
        {
            return await _context.GoodsReceipts
                .AsNoTracking()
                .Include(x => x.Items)
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<GoodsReceipt?> GetByIdAsync(int id)
        {
            return await _context.GoodsReceipts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<GoodsReceipt> CreateAsync(GoodsReceipt receipt)
        {
            await _context.GoodsReceipts.AddAsync(receipt);
            await _context.SaveChangesAsync();

            return receipt;
        }

        public async Task<bool> UpdateAsync(GoodsReceipt receipt)
        {
            var existing = await _context.GoodsReceipts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == receipt.Id);

            if (existing == null)
                return false;

            existing.ReceiptNumber = receipt.ReceiptNumber;
            existing.PurchaseOrderId = receipt.PurchaseOrderId;
            existing.ReceivedDate = receipt.ReceivedDate;
            existing.Status = receipt.Status;
            existing.Notes = receipt.Notes;

            // Replace the receipt's item collection.
            existing.Items.Clear();

            foreach (var item in receipt.Items)
            {
                existing.Items.Add(new GoodsReceiptItem
                {
                    ProductName = item.ProductName,
                    OrderedQuantity = item.OrderedQuantity,
                    ReceivedQuantity = item.ReceivedQuantity,
                    Remarks = item.Remarks
                });
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var receipt = await _context.GoodsReceipts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (receipt == null)
                return false;

            _context.GoodsReceipts.Remove(receipt);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}