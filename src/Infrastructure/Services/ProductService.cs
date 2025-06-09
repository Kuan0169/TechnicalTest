﻿using Microsoft.EntityFrameworkCore;
using MyCompany.Test.Core;
using MyCompany.Test.Core.Entities;
using MyCompany.Test.Infrastructure.Models;


namespace MyCompany.Test.Infrastructure.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(Guid id);
        Task<ProductDto> CreateAsync(ProductModel productModel);
        Task UpdateAsync(Guid id, ProductModel productModel);
        Task DeleteAsync(Guid id);

    }
    public class ProductService : IProductService
    {
        private readonly ProductContext context;

        public ProductService(ProductContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            return await context.Product.Select(p => new ProductDto
            {
                Id = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
            }).ToListAsync();
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var product = await context.Product.Where(p => p.ProductId == id).Select(c => new ProductDto
            {
                Id = c.ProductId,
                Name = c.Name,
                Price = c.Price,
                Description = c.Description,
                CreatedAt= c.CreatedAt,
            }).FirstOrDefaultAsync();

            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task<ProductDto> CreateAsync(ProductModel productModel)
        {
            var product = new Product
            {
                ProductId = Guid.NewGuid(),
                Name = productModel.Name,
                Price = productModel.Price,
                Description = productModel.Description,
                CreatedAt = productModel.CreatedAt,
            };
            context.Product.Add(product);
            await context.SaveChangesAsync();
            var productDto = new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Price = productModel.Price,
                Description = productModel.Description,
                CreatedAt = productModel.CreatedAt,
            };
            return productDto;
        }

        public async Task UpdateAsync(Guid id, ProductModel productModel)
        {
            var product = await context.Product.Where(c => c.ProductId == id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.ProductId = id;
            product.Name = productModel.Name;
            product.Price = productModel.Price;
            product.Description = productModel.Description;
            product.CreatedAt = productModel.CreatedAt;

            context.Product.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await context.Product.FirstOrDefaultAsync(p => p.ProductId == id);
            context.Product.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
