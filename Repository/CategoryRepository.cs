using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        SuperMarket214338766Context _superMarketContext;

        public CategoryRepository(SuperMarket214338766Context superMarketContext)
        {
            _superMarketContext = superMarketContext;

        }
        public async Task<IEnumerable<Category>> getAllProductsAsync()
        {
            return await _superMarketContext.Categories.ToListAsync();
        }
    }
}
