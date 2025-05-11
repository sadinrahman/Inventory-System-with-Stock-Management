using InventoryManagment.Dto;
using InventoryManagment.Models;

namespace InventoryManagment.Service.ProductServices
{
	public interface IProductService
	{
		Task<bool> AddProductAsync(ProductDTO productDTO, Guid userid);
	}
}
