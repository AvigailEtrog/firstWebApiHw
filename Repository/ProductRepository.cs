using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ProductRepository :IProductRepository
    {
        SuperMarket214338766Context _superMarketContext;

        public ProductRepository(SuperMarket214338766Context superMarketContext)
        {
            _superMarketContext = superMarketContext;

        }
        public async Task<List<Product>> getAllProductsAsync(string?desc,int? minPrice,int? maxPrice,int?[] categoryIds)
        {
            var query = _superMarketContext.Products.Where(product =>
            (desc == null ? (true) : (product.ProductDescription.Contains(desc)))
            && (minPrice == null ? (true) : (product.ProductPrice >= minPrice))
            && (maxPrice == null ? (true) : (product.ProductPrice <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId)))).Include(product => product.Category)
                    .OrderBy(product => product.ProductPrice);
            List<Product>products= await query.ToListAsync();
            return products;
        }
        public async Task<List<Product>> getCertainProductsAsync(int[] productsIds)
        {
            var query = _superMarketContext.Products.Where(product => productsIds.Contains(product.ProductId));
            List<Product> products = await query.ToListAsync();
            return products;
        }


    }
}
