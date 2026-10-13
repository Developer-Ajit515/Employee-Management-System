
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly AppDbContext _context;

        public PurchaseOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _context.Set<PurchaseOrder>()
                .AsNoTracking()
                .Include(p => p.Supplier)
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<PurchaseOrder?> GetByIdAsync(int id)
        {
            return await _context.Set<PurchaseOrder>()
                .AsNoTracking()
                .Include(p => p.Supplier)
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PurchaseOrder> CreateAsync(
            PurchaseOrder purchaseOrder)
        {
            // Verify that the supplier exists.
            bool supplierExists = await _context.Set<Supplier>()
                .AnyAsync(s => s.Id == purchaseOrder.SupplierId);

            if (!supplierExists)
            {
                throw new ArgumentException(
                    $"Supplier ID {purchaseOrder.SupplierId} does not exist.");
            }

            // Generate a PO number on the server.
            purchaseOrder.Id = 0;
            purchaseOrder.PONumber =
                $"PO-{Guid.NewGuid().ToString("N")[..12].ToUpperInvariant()}";

            // Use the existing supplier through SupplierId.
            purchaseOrder.Supplier = null;

            purchaseOrder.CreatedDate = DateTime.UtcNow;
            purchaseOrder.UpdatedDate = null;

            await _context.Set<PurchaseOrder>()
                .AddAsync(purchaseOrder);

            await _context.SaveChangesAsync();

            return purchaseOrder;
        }

        public async Task<bool> UpdateAsync(
            int id,
            PurchaseOrder purchaseOrder)
        {
            var existingOrder = await _context.Set<PurchaseOrder>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingOrder == null)
                return false;

            bool supplierExists = await _context.Set<Supplier>()
                .AnyAsync(s => s.Id == purchaseOrder.SupplierId);

            if (!supplierExists)
            {
                throw new ArgumentException(
                    $"Supplier ID {purchaseOrder.SupplierId} does not exist.");
            }

            // Never change the primary key or replace the tracked entity.
            existingOrder.PONumber = purchaseOrder.PONumber;
            existingOrder.PODate = purchaseOrder.PODate;
            existingOrder.ExpectedDeliveryDate =
                purchaseOrder.ExpectedDeliveryDate;

            existingOrder.SupplierId = purchaseOrder.SupplierId;
            existingOrder.Status = purchaseOrder.Status;

            existingOrder.SubTotal = purchaseOrder.SubTotal;
            existingOrder.DiscountAmount = purchaseOrder.DiscountAmount;
            existingOrder.TaxAmount = purchaseOrder.TaxAmount;
            existingOrder.ShippingAmount = purchaseOrder.ShippingAmount;
            existingOrder.OtherCharges = purchaseOrder.OtherCharges;
            existingOrder.GrandTotal = purchaseOrder.GrandTotal;

            existingOrder.PaymentTerms = purchaseOrder.PaymentTerms;
            existingOrder.DeliveryAddress = purchaseOrder.DeliveryAddress;
            existingOrder.Notes = purchaseOrder.Notes;
            existingOrder.TermsAndConditions =
                purchaseOrder.TermsAndConditions;

            existingOrder.IsActive = purchaseOrder.IsActive;
            existingOrder.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var purchaseOrder = await _context.Set<PurchaseOrder>()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (purchaseOrder == null)
                return false;

            _context.Set<PurchaseOrder>().Remove(purchaseOrder);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

