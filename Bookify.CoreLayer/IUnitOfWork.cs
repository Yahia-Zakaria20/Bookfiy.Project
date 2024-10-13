using Bookify.CoreLayer.Entites;
using Bookify.CoreLayer.RepositoryLayer.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.CoreLayer
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		IGenericRepository<Category> CategoryRepo { get; set; }


		Task<int> Complete();

	}
}
