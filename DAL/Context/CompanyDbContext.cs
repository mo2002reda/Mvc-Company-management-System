using DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
	public class CompanyDbContext : IdentityDbContext<ApplicationUser>
	{
		public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
		{
		}
		public DbSet<Department> departments { get; set; }
		public DbSet<Employee> employees { get; set; }

	}
}
