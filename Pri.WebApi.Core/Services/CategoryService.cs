using Pri.CleanArchitecture.Core.Entities;
using Pri.CleanArchitecture.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Core.Interfaces.Services;
using Pri.CleanArchitecture.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResultModel<Category>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if(category == null)
            {
                return new ResultModel<Category>
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "Category not found!" }
                };
            }
            return new ResultModel<Category>
            {
                IsSuccess = true,
                Result = category,
            };
        }
    }
}
