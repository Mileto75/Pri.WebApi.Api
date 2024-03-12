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
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(ApplicationDbContext applicationDbContext, ILogger<BaseRepository<Property>> logger) : base(applicationDbContext, logger)
        {
        }
    }
}
