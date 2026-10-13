using ERP.APPLICATIONBUSINESS.Interfaces;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTURE.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // GET ALL PAYMENTS
        // =========================================================

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Invoice)
                .Include(p => p.Customer)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }


        // =========================================================
        // GET PAYMENT BY ID
        // =========================================================

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Invoice)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(
                    p => p.PaymentId == id &&
                         p.IsActive);
        }


        // =========================================================
        // GET PAYMENTS BY INVOICE
        // =========================================================

        public async Task<IEnumerable<Payment>> GetByInvoiceIdAsync(
            int invoiceId)
        {
            return await _context.Payments
                .Include(p => p.Invoice)
                .Include(p => p.Customer)
                .Where(p =>
                    p.InvoiceId == invoiceId &&
                    p.IsActive)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }


        // =========================================================
        // GET PAYMENTS BY CUSTOMER
        // =========================================================

        public async Task<IEnumerable<Payment>> GetByCustomerIdAsync(
            int customerId)
        {
            return await _context.Payments
                .Include(p => p.Invoice)
                .Include(p => p.Customer)
                .Where(p =>
                    p.CustomerId == customerId &&
                    p.IsActive)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }


        // =========================================================
        // GET TOTAL COMPLETED PAYMENT FOR INVOICE
        // =========================================================

        public async Task<decimal> GetTotalPaidForInvoiceAsync(
            int invoiceId)
        {
            return await _context.Payments
                .Where(p =>
                    p.InvoiceId == invoiceId &&
                    p.IsActive &&
                    p.Status == "Completed")
                .SumAsync(p => (decimal?)p.Amount) ?? 0m;
        }


        // =========================================================
        // ADD PAYMENT
        // =========================================================

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }


        // =========================================================
        // UPDATE PAYMENT
        // =========================================================

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);

            await Task.CompletedTask;
        }


        // =========================================================
        // DELETE PAYMENT
        // =========================================================

        public async Task DeleteAsync(Payment payment)
        {
            // Soft delete
            payment.IsActive = false;
            payment.UpdatedAt = DateTime.UtcNow;

            _context.Payments.Update(payment);

            await Task.CompletedTask;
        }


        // =========================================================
        // CHECK PAYMENT EXISTS
        // =========================================================

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Payments
                .AnyAsync(p =>
                    p.PaymentId == id &&
                    p.IsActive);
        }


        // =========================================================
        // SAVE CHANGES
        // =========================================================

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}