using Bookify.CoreLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.RepositoryLayer.AppData
{
	public class StoreDbContext:DbContext
	{
        public StoreDbContext(DbContextOptions<StoreDbContext> options):base(options)
        {
            
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		}


		public DbSet<Category> Categories { get; set; }
    }
}
