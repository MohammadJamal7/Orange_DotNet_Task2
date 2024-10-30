using Microsoft.EntityFrameworkCore;
using Orange_DotNet_Task2.Models;

namespace Orange_DotNet_Task2.Data
{
	public class ApplicationContext :DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
	}
}
