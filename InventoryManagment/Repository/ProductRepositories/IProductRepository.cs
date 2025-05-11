using InventoryManagment.Models;
using System.Threading.Tasks;

namespace InventoryManagment.Repository.ProductRepositories
{
	public interface IProductRepository
	{
		Task<List<Product>> GetAllAsync();
		Task<bool> AddProductAsync(Product product);
		Task<Product> GetProductById(Guid id);
		Task<bool> UpdateProductAsync(Product product);
	}
}
