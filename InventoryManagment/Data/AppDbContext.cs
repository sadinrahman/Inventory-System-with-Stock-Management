using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagment.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductVariant> ProductVariants { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ProductSubVariant> ProductSubVariants { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Product>()
				.HasOne(p => p.Category)
				.WithMany(c => c.Products)
				.HasForeignKey(p => p.CategoryId);
			modelBuilder.Entity<Product>()
				.HasOne(p => p.User)
				.WithMany(u => u.Products)
				.HasForeignKey(p => p.UserId);
			modelBuilder.Entity<ProductVariant>()
				.HasOne(pv => pv.Product)
				.WithMany(p => p.Variants)
				.HasForeignKey(pv => pv.ProductId);
			modelBuilder.Entity<ProductSubVariant>()
				.HasOne(psv => psv.ProductVariant)
				.WithMany(pv => pv.SubVariants)
				.HasForeignKey(psv => psv.ProductVariantId);
			modelBuilder.Entity<Product>()
			.HasMany(p => p.Variants)
			.WithOne(v => v.Product)
			.HasForeignKey(v => v.ProductId)
			.OnDelete(DeleteBehavior.Cascade);

			// ProductVariant → ProductSubVariants
			modelBuilder.Entity<ProductVariant>()
				.HasMany(v => v.SubVariants)
				.WithOne(sv => sv.ProductVariant)
				.HasForeignKey(sv => sv.ProductVariantId)
				.OnDelete(DeleteBehavior.Cascade);

		}
	}
}
	