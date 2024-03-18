using Pri.CleanArchitecture.Core.Entities;
using Pri.CleanArchitecture.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ResultModel<IEnumerable<Product>>> GetAllAsync();
        IQueryable<Product> GetAll();
        Task<bool> ExistsAsync(int id);
        Task<ResultModel<Product>> GetByIdAsync(int id);
        Task<ResultModel<Product>>
            CreateAsync(string name,int categoryId,string description,decimal price, IEnumerable<int> propertyIds);
        Task<ResultModel<Product>> UpdateAsync(int id,string name, int categoryId, string description, decimal price, IEnumerable<int> propertyIds);
        Task<ResultModel<Product>> DeleteAsync(int id);
        Task<ResultModel<IEnumerable<Product>>> SearchByNameAsync(string name);
        Task<ResultModel<IEnumerable<Product>>> SearchByCategoryIdAsync(int categoryId);
    }
}
