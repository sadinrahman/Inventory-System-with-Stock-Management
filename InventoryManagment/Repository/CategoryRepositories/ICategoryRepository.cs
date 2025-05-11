

using InventoryManagment.Models;

namespace InventoryManagment.Repository.CategoryRepositories
{
	public interface ICategoryRepository
	{
		Task<List<Category>> GetAllAsync();
		Task<bool> AddCategoryAsync(Category category);
		Task<Category> GetCategoryById(Guid id);
	}
}
