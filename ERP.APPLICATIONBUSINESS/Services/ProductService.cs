using AutoMapper;
using ERP.APPLICATIONBUSINESS.Exceptions;
using ERP.APPLICATIONBUSINESS.Interfaces;
using ERP.APPLICATIONBUSINESS.Interfaces.IServices;
using ERP.DOMAINMODELS.Dtos;
using ERP.DOMAINMODELS.Entities;

namespace ERP.APPLICATIONBUSINESS.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuditLogService _auditLogService;


        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper, 
            IAuditLogService auditLogService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _auditLogService = auditLogService;
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            var products = await _unitOfWork.Products.GetAll();

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetById(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);

            if (product == null)
                throw new NotFoundException("Product not found");

            return _mapper.Map<ProductDto>(product);
        }

        public async Task Add(CreateProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            await _unitOfWork.Products.Add(product);

            await _auditLogService.AddAsync(new AuditLog
            {
                UserId = null, // later JWT user ID se lenge

                Action = "Created",

                EntityName = "Product",

                EntityId = product.Id.ToString(),

                Description = $"Product '{product.Name}' was created.",

                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Update(UpdateProductDto dto)
        {
            var existingProduct = await _unitOfWork.Products.GetById(dto.Id);

            if (existingProduct == null)
                throw new NotFoundException("Product not found");

            _mapper.Map(dto, existingProduct);

            _unitOfWork.Products.Update(existingProduct);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);

            if (product == null)
                throw new NotFoundException("Product not found");

            _unitOfWork.Products.Delete(product);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetLowStockProducts(int threshold)
        {
            var products = await _unitOfWork.Products
                .GetLowStockProducts(threshold);

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}