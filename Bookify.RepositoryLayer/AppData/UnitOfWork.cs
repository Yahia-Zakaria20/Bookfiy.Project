using Bookify.CoreLayer;
using Bookify.CoreLayer.Entites;
using Bookify.CoreLayer.RepositoryLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.RepositoryLayer.AppData
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext _dbContext;

		public IGenericRepository<Category> CategoryRepo { get; set; }

		public UnitOfWork(StoreDbContext dbContext) 
		{
			CategoryRepo = new GenericRepository<Category>(dbContext);
			_dbContext = dbContext;
		}
		public async Task<int> CompleteAsync()
		{
		   return	await _dbContext.SaveChangesAsync();
		}

		public async ValueTask DisposeAsync()
		{
			await _dbContext.DisposeAsync();
		}
	}
}
