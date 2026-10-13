
using ERP.APPLICATIONBUSINESS.Interfaces.IRepository;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _repository;

        public PurchaseOrderService(
            IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PurchaseOrder?> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "Invalid purchase order ID.");

            return await _repository.GetByIdAsync(id);
        }

        public async Task<PurchaseOrder> CreateAsync(
            PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null)
                throw new ArgumentNullException(nameof(purchaseOrder));

            if (purchaseOrder.SupplierId <= 0)
                throw new ArgumentException(
                    "Please select a valid supplier.");

            if (string.IsNullOrWhiteSpace(purchaseOrder.PONumber))
                throw new ArgumentException(
                    "Purchase order number is required.");

            if (purchaseOrder.GrandTotal < 0 ||
                purchaseOrder.SubTotal < 0 ||
                purchaseOrder.DiscountAmount < 0 ||
                purchaseOrder.TaxAmount < 0 ||
                purchaseOrder.ShippingAmount < 0 ||
                purchaseOrder.OtherCharges < 0)
            {
                throw new ArgumentException(
                    "Financial amounts cannot be negative.");
            }

            return await _repository.CreateAsync(purchaseOrder);
        }

        public async Task<bool> UpdateAsync(
            int id,
            PurchaseOrder purchaseOrder)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "Invalid purchase order ID.");

            if (purchaseOrder == null)
                throw new ArgumentNullException(nameof(purchaseOrder));

            if (purchaseOrder.SupplierId <= 0)
                throw new ArgumentException(
                    "Please select a valid supplier.");

            if (purchaseOrder.GrandTotal < 0 ||
                purchaseOrder.SubTotal < 0 ||
                purchaseOrder.DiscountAmount < 0 ||
                purchaseOrder.TaxAmount < 0 ||
                purchaseOrder.ShippingAmount < 0 ||
                purchaseOrder.OtherCharges < 0)
            {
                throw new ArgumentException(
                    "Financial amounts cannot be negative.");
            }

            return await _repository.UpdateAsync(id, purchaseOrder);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "Invalid purchase order ID.");

            return await _repository.DeleteAsync(id);
        }
    }
}

