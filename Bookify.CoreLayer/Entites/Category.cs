using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.CoreLayer.Entites
{
	public class Category:BaseEntite
	{
		public string Name { get; set; } = null!;

		public bool IsDeleted { get; set; }


		public DateTime DateOfCreation { get; set; } = DateTime.Now;


		public DateTime? LastUpdate { get; set; }

	}
}
