using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.DOMAINMODELS.Entities;
using ERP.INFRASTRUCTUREDATA.Data;
using Microsoft.EntityFrameworkCore;

namespace ERP.INFRASTRUCTUREDATA.Repositories
{
    public class SalesOrderRepository : ISalesOrderRepository
    {
        private readonly AppDbContext _context;

        public SalesOrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesOrder>> GetAllAsync()
        {
            return await _context.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<SalesOrder?> GetByIdAsync(int id)
        {
            var order = await _context.SalesOrders
                .Include(x => x.Customer)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order != null)
            {
                Console.WriteLine(
                    $"Customer: {order.Customer?.Name}");

                foreach (var item in order.Items)
                {
                    Console.WriteLine(
                        $"Product: {item.Product?.Name}");
                }
            }

            return order;
        }

        public async Task<SalesOrder> AddAsync(
            SalesOrder salesOrder)
        {
            await _context.SalesOrders.AddAsync(salesOrder);

            await _context.SaveChangesAsync();

            return salesOrder;
        }

        public async Task<SalesOrder?> UpdateAsync(
            SalesOrder salesOrder)
        {
            var existingOrder = await _context.SalesOrders
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.Id == salesOrder.Id);

            if (existingOrder == null)
                return null;

            existingOrder.CustomerId = salesOrder.CustomerId;
            existingOrder.OrderDate = salesOrder.OrderDate;
            existingOrder.Status = salesOrder.Status;
            existingOrder.Subtotal = salesOrder.Subtotal;
            existingOrder.DiscountAmount = salesOrder.DiscountAmount;
            existingOrder.TaxAmount = salesOrder.TaxAmount;
            existingOrder.TotalAmount = salesOrder.TotalAmount;
            existingOrder.Notes = salesOrder.Notes;

            _context.SalesOrderItems.RemoveRange(
                existingOrder.Items);

            existingOrder.Items = salesOrder.Items;

            await _context.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _context.SalesOrders
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
                return false;

            _context.SalesOrders.Remove(order);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}