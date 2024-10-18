using Bookify.CoreLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.CoreLayer.RepositoryLayer.Contract
{
	public interface IGenericRepository<T>where T : BaseEntite
	{

	  public Task<IEnumerable<T>> GetAllAsync();

		public void Add(T Entity);

		public void Update(T Entity);

		public Task<T?> GetByIdAsync(int id);	
	}
}
