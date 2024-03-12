using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pri.CleanArchitecture.Core.Entities;
using Pri.CleanArchitecture.Core.Interfaces.Repositories;
using Pri.CleanArchitecture.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDbContext, ILogger<BaseRepository<Product>> logger) : base(applicationDbContext, logger)
        {
        }

        public override IQueryable<Product> GetAll()
        {
            return _table.Include(p => p.Category)
                .Include(p => p.Properties).AsQueryable();
        }

        public override async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _table.Include(p => p.Category)
                .Include(p => p.Properties).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryIdAsync(int categoryId)
        {
            return await _table
                .Include(p => p.Category)
                .Include(p => p.Properties)
                .Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _table.Include(p => p.Category)
                .Include(p => p.Properties)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
