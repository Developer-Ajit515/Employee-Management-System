using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                .Include(x => x.Customer)
                .Include(x => x.InvoiceItems)
                    .ThenInclude(x => x.Product)
                .OrderByDescending(x => x.InvoiceId)
                .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(int id)
        {
            return await _context.Invoices
                .Include(x => x.Customer)
                .Include(x => x.InvoiceItems)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.InvoiceId == id);
        }

        public async Task<Invoice> CreateAsync(Invoice invoice)
        {
            _context.Invoices.Add(invoice);

            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<Invoice> UpdateAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);

            await _context.SaveChangesAsync();

            return invoice;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var invoice = await _context.Invoices
                .FirstOrDefaultAsync(x => x.InvoiceId == id);

            if (invoice == null)
                return false;

            invoice.IsActive = false;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}