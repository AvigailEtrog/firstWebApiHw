using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> getAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            List<Product> products = await _productRepository.getAllProducts( desc,  minPrice,maxPrice,categoryIds);
            return products != null ? products:null  ;


        }
       
    }
}
