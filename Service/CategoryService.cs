﻿using Entities;
using Repositories;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            IEnumerable<Category> categories = await _categoryRepository.getAllProducts();
            return categories != null ? null : categories;
            

        }

    }
}
