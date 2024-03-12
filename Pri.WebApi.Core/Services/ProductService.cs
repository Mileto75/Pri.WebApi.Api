using Microsoft.EntityFrameworkCore;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPropertyRepository _propertyRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IPropertyRepository propertyRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _propertyRepository = propertyRepository;
        }

        public async Task<ResultModel<Product>> CreateAsync(string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds)
        {
            //check if product name exists
            if (await _productRepository
                .GetAll()
                .AnyAsync(p => p.Name.ToUpper() == name.ToUpper()))
            {
                return new ResultModel<Product>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Name exists!" }
                };
            }
            //check if categoryId exists
            if (!await _categoryRepository.GetAll().AnyAsync(c => c.Id == categoryId))
            {
                return new ResultModel<Product>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Unknown category!" }
                };
            }
            //check if propertyIds exist
            if(propertyIds.Distinct().Count() != _propertyRepository.GetAll().Where(pr => propertyIds.Contains(pr.Id)).Count())
            {
                return new ResultModel<Product>
                {
                    IsSuccess = false,
                    Errors = new List<string> {"Property does not exist!"}
                };
            }
            //create the entity
            //call the repo method
            Product newProduct = new Product
            {
                Name = name,
                Price = price,
                CategoryId = categoryId,
                Properties = await _propertyRepository
                .GetAll()
                .Where(p => propertyIds.Contains(p.Id)).ToListAsync(),
                Description = description,
            };
            if (await _productRepository.CreateAsync(newProduct))
            {
                return new ResultModel<Product>
                {
                    IsSuccess = true,
                    Result = await _productRepository.GetByIdAsync(newProduct.Id)
                };
            }
            return new ResultModel<Product>
            {
                IsSuccess = false,
                Errors = new List<string> { "Product not created!" }
            };
        }

        public Task<ResultModel<Product>> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultModel<IEnumerable<Product>>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            if (products.Count() > 0)
            {
                return new ResultModel<IEnumerable<Product>>
                {
                    IsSuccess = true,
                    Result = products
                };
            }
            return new ResultModel<IEnumerable<Product>>
            {
                IsSuccess = false,
                Errors = new List<string> { "No products found!" }
            };
        }

        public async Task<ResultModel<Product>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new ResultModel<Product>
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Product not found!" }
                };
            }
            return new ResultModel<Product>
            {
                IsSuccess = true,
                Result =  product
            };
        }

        public async Task<ResultModel<IEnumerable<Product>>> SearchByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryIdAsync(categoryId);
            if (products.Count() > 0)
            {
                return new ResultModel<IEnumerable<Product>>
                {
                    IsSuccess = true,
                    Result = products
                };
            }
            return new ResultModel<IEnumerable<Product>>
            {
                IsSuccess = false,
                Errors = new List<string> { "No products found!" }
            };
        }

        public async Task<ResultModel<IEnumerable<Product>>> SearchByNameAsync(string name)
        {
            var products = await _productRepository.GetAll()
                .Include(p => p.Category)
                .Include(p => p.Properties)
                .Where(p => p.Name.ToUpper().Contains(name.ToUpper()))
                .ToListAsync();
            if (products.Count() > 0)
            {
                return new ResultModel<IEnumerable<Product>>
                {
                    IsSuccess = true,
                    Result = products
                };
            }
            return new ResultModel<IEnumerable<Product>>
            {
                IsSuccess = false,
                Errors = new List<string> { "No products found!" }
            };
        }

        public Task<ResultModel<Product>> UpdateAsync(int id, string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds)
        {
            throw new NotImplementedException();
        }
    }
}
