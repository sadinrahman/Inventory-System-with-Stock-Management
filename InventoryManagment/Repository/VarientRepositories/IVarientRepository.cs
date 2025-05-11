using InventoryManagment.Models;

namespace InventoryManagment.Repository.VarientRepositories
{
	public interface IVarientRepository
	{
		Task<bool> AddVarientAsync(ProductVariant varient);
		Task<ProductVariant> ProductVariantById(Guid id);
		Task<bool> AddSubVarient(ProductSubVariant subVarient);
		Task<ProductSubVariant> GetSubVarientById(Guid id);
	}
}
