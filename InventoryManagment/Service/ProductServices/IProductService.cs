using InventoryManagment.Dto;
using InventoryManagment.Models;

namespace InventoryManagment.Service.ProductServices
{
	public interface IProductService
	{
		Task<bool> AddProductAsync(ProductDTO productDTO, Guid userid);
		Task<List<ProductViewDto>> GetAllAsync();
		Task<string> AddStockAsync(ProductStockUpdateDto productStock,Guid userid);
		Task<string> RemoveStockAsync(ProductStockUpdateDto productStock);
	}
}
