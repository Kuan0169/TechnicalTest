using MyCompany.Test.Core.Entities;

namespace MyCompany.Test.Infrastructure.Services
{

    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product input);
        Task<bool> UpdateAsync(int id, Product input);
        Task<bool> DeleteAsync(int id);

    }

    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public Task<IEnumerable<Product>> GetAllAsync() =>
            Task.FromResult(_products.AsEnumerable());

        public Task<Product?> GetByIdAsync(int id) =>
            Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public Task<Product> CreateAsync(Product input)
        {
            input.Id = _nextId++;
            input.CreatedAt = DateTime.UtcNow;
            _products.Add(input);
            return Task.FromResult(input);
        }

        public Task<bool> UpdateAsync(int id, Product input)
        {
            var existing = _products.FirstOrDefault(p => p.Id == id);
            if (existing == null) return Task.FromResult(false);

            existing.Name = input.Name;
            existing.Description = input.Description;
            existing.Price = input.Price;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null) return Task.FromResult(false);

            _products.Remove(product);
            return Task.FromResult(true);
        }
    }
}
