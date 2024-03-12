using Pri.CleanArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pri.CleanArchitecture.Core.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        Task<bool> CreateAsync(T toAdd);
        Task<bool> UpdateAsync(T toUpdate);
        Task<bool> DeleteAsync(T toDelete);
    }
}
