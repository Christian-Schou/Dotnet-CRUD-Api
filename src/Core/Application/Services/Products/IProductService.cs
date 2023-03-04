using Application.Services.Common;
using Domain.Entitites.Products;

namespace Application.Services.Products
{
    public interface IProductService : ITransientService
    {
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(Guid categoryId);
        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid productId);
    }
}
