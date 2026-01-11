using ARMForge.Business.Interfaces;
using ARMForge.Kernel.Entities;
using ARMForge.Kernel.Interfaces.GenericRepository;
using ARMForge.Kernel.Interfaces.UnitOfWork;
using ARMForge.Types.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARMForge.Business.Services
{
    public class ProductService(
        IGenericRepository<Product> productRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService) : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly ICurrentUserService _currentUserService = currentUserService;

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllWithIncludesAsync(
                p => p.OrderItems
            );

            if (!_currentUserService.IsAdmin)
            {
                products = products.Where(p => p.IsActive).ToList();
            }

            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetByConditionAsync(
                p => p.Id == id && (_currentUserService.IsAdmin || p.IsActive),
                include: q => q.Include(p => p.OrderItems)
            );

            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddProductAsync(ProductCreateDto productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Category))
                throw new InvalidOperationException("Kategori zorunludur.");

            productDto.StockKeepingUnit = GenerateSku(
                productDto.Name,
                productDto.Category
            );

            var product = _mapper.Map<Product>(productDto);
            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, ProductUpdateDto productDto)
        {
            var product = await _productRepository.GetByConditionAsync(p => p.Id == id);
            if (product == null)
                return null;

            if (!string.IsNullOrEmpty(productDto.StockKeepingUnit) && productDto.StockKeepingUnit != product.StockKeepingUnit)
            {
                var skuExists = await _productRepository.FindAsync(p => p.StockKeepingUnit == productDto.StockKeepingUnit);
                if (skuExists.Any())
                    throw new InvalidOperationException("Bu SKU numarası zaten mevcut.");
            }

            if (productDto.UnitPrice.HasValue && productDto.UnitPrice < 0)
                throw new InvalidOperationException("Ürün fiyatı negatif olamaz.");

            if (productDto.StockQuantity.HasValue && productDto.StockQuantity < 0)
                throw new InvalidOperationException("Stok miktarı negatif olamaz.");

            _mapper.Map(productDto, product);
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByConditionAsync(p => p.Id == id && p.IsActive);
            if (product == null)
                return false;

            product.IsActive = false;
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);
            await _unitOfWork.CommitAsync();
            return true;
        }

        private static string GenerateSku(string name, string category)
        {
            var prefix = category?[..3].ToUpper() ?? "GEN";
            var namePart = name.Replace(" ", "")[..5].ToUpper();
            var random = new Random().Next(1000, 9999);

            return $"{prefix}-{namePart}-{random}"; // "ELE-IPHON-3847"
        }
    }
}
