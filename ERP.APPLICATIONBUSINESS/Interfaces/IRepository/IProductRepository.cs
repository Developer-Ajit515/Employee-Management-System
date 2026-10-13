using ERP.DOMAINMODELS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.APPLICATIONBUSINESS.Interfaces.IRepository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        
        Task<IEnumerable<Product>> GetLowStockProducts(int threshold);
        
    }
}
