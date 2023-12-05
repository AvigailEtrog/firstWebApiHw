using Entities;

namespace Services
{
    public interface IProductService
    {
        Task<List<Product>> getAllProductsAsync(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
    }
}