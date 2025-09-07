using ARMForge.Kernel.Entities;
using ARMForge.Types.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMForge.Types.Mapping
{
    public class ProductMapper
    {
        // Entity'den DTO'ya dönüşüm
        public static ProductDto ToDto(Product entity)
        {
            if (entity == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = entity.Id,
                Name = entity.Name,
                StockKeepingUnit = entity.StockKeepingUnit,
                Description = entity.Description,
                UnitPrice = entity.UnitPrice,
                StockQuantity = entity.StockQuantity,
                Category = entity.Category
            };
        }

        // DTO'dan Entity'ye dönüşüm (Yeni bir nesne oluşturmak için)
        public static Product ToEntity(ProductCreateDto dto)
        {
            if (dto == null)
            {
                return null;
            }

            return new Product
            {
                Name = dto.Name,
                StockKeepingUnit = dto.StockKeepingUnit,
                Description = dto.Description,
                UnitPrice = dto.UnitPrice,
                StockQuantity = dto.StockQuantity,
                Category = dto.Category
            };
        }

        // DTO'dan Entity'ye dönüşüm (Mevcut bir nesneyi güncellemek için)
        public static void ToEntity(ProductUpdateDto dto, Product entity)
        {
            if (dto == null || entity == null)
            {
                return;
            }

            entity.Name = dto.Name;
            entity.StockKeepingUnit = dto.StockKeepingUnit;
            entity.Description = dto.Description;
            entity.UnitPrice = dto.UnitPrice;
            entity.StockQuantity = dto.StockQuantity;
            entity.Category = dto.Category;
        }
    }
}
