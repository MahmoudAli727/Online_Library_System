using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Data
{
	public class AppDbContext : IdentityDbContext<AppUser>
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}
		public DbSet<Book> books { get; set; }
		public DbSet<Order> orders { get; set; }
		public DbSet<BookCategory>bookCategories { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityRole>().HasData(
			   new IdentityRole
			   {
				   Id = Guid.NewGuid().ToString(),
				   Name = "User",
				   NormalizedName = "USER"
			   },
			   new IdentityRole
			   {
				   Id = Guid.NewGuid().ToString(),
				   Name = "Librarian",
				   NormalizedName = "LIBRARIAN"
			   }
		   );

		}
	}
}
