using InventoryManagment.Data;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;



namespace InventoryManagment.Repository.CategoryRepositories
{
	public class CategoryRepository: ICategoryRepository
	{
		private readonly AppDbContext _context;
		public CategoryRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<List<Category>> GetAllAsync()
		{
			return await _context.Categories.ToListAsync();
		}
		public async Task<bool> AddCategoryAsync(Category category)
		{
			await _context.Categories.AddAsync(category);
			return await _context.SaveChangesAsync() > 0;
		}
		
		public async Task<Category> GetCategoryById(Guid id)
		{
			return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
