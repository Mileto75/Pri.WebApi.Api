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
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;
        protected readonly DbSet<T> _table;
        private ILogger<BaseRepository<T>> _logger;

        public BaseRepository(ApplicationDbContext applicationDbContext, ILogger<BaseRepository<T>> logger)
        {
            _applicationDbContext = applicationDbContext;
            _table = _applicationDbContext.Set<T>();
            _logger = logger;
        }
        public async Task<bool> CreateAsync(T toAdd)
        {
            _table.Add(toAdd);
            return await SaveChanges();
        }

        public async Task<bool> DeleteAsync(T toDelete)
        {
            _table.Remove(toDelete);
            return await SaveChanges();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _table.AsQueryable();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _table.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateAsync(T toUpdate)
        {
            _table.Update(toUpdate);
            return await SaveChanges();
        }
        private async Task<bool> SaveChanges()
        {
            try
            {
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(dbUpdateException.Message);
                return false;
            }
        }

    }
}
