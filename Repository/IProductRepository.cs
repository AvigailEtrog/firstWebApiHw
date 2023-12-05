using Entities;

namespace Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> getAllProductsAsync(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
        Task<List<Product>> getCertainProductsAsync(int[] productsIds);
    }
}