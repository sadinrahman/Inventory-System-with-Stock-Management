using InventoryManagment.Data;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagment.Repository.ProductRepositories
{
	public class ProductRepository:IProductRepository
	{
		private readonly AppDbContext _context;
		public ProductRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Product>> GetAllAsync()
		{
			return await _context.Products.Include(x=>x.Variants).ThenInclude(p=>p.SubVariants).Include(c=>c.Category).Include(c=>c.User).ToListAsync();
		}
		public async Task<bool> AddProductAsync(Product product)
		{
			await _context.Products.AddAsync(product);
			var entries = _context.ChangeTracker.Entries();
			foreach (var entry in entries)
			{
				Console.WriteLine($"{entry.Entity.GetType().Name}: {entry.State}");
			}

			return await _context.SaveChangesAsync() > 0;
		}

		public async Task<Product> GetProductById(Guid id)
		{
			return await _context.Products.Include(x => x.Variants).ThenInclude(p => p.SubVariants).Include(c => c.Category).Include(x=>x.User).FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<bool> UpdateProductAsync(Product product)
		{
			_context.Products.Update(product);
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
