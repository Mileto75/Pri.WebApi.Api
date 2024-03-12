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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        
        public CategoryRepository(ApplicationDbContext applicationDbContext, ILogger<BaseRepository<Category>> logger) : base(applicationDbContext, logger)
        {
        }
    }
}
