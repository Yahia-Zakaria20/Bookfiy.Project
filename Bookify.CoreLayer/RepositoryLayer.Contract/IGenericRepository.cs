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
	}
}
