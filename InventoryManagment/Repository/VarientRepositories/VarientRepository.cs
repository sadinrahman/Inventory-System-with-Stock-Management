using InventoryManagment.Data;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagment.Repository.VarientRepositories
{
	public class VarientRepository: IVarientRepository
	{
		private readonly AppDbContext _context;
		public VarientRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<bool> AddVarientAsync(ProductVariant varient)
		{
			await _context.ProductVariants.AddAsync(varient);
			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<ProductVariant> ProductVariantById(Guid id)
		{
			return await _context.ProductVariants.FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<bool> AddSubVarient(ProductSubVariant subVarient)
		{
			await _context.ProductSubVariants.AddAsync(subVarient);
			return await _context.SaveChangesAsync() > 0;
		}
		public async Task<ProductSubVariant> GetSubVarientById(Guid id)
		{
			return await _context.ProductSubVariants.FirstOrDefaultAsync(x => x.Id == id);
		}
	}
}
