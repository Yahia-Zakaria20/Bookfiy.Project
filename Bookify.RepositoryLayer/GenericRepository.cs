using Bookify.CoreLayer.Entites;
using Bookify.CoreLayer.RepositoryLayer.Contract;
using Bookify.RepositoryLayer.AppData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.RepositoryLayer
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntite
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()
         => await _dbContext.Set<T>().ToListAsync();

    }
}
