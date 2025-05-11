using InventoryManagment.Dto;
using InventoryManagment.Models;
using InventoryManagment.Repository.ProductRepositories;
using InventoryManagment.Repository.VarientRepositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace InventoryManagment.Service.ProductServices
{
	public class ProductService:IProductService
	{
		private readonly IProductRepository _repository;
		private readonly IVarientRepository _varientRepository;
		public ProductService(IProductRepository repository,IVarientRepository varientRepository)
		{
			_repository = repository;
			_varientRepository = varientRepository;
		}
		public async Task<List<Product>> GetAllAsync()
		{
			return await _repository.GetAllAsync();

		}
		public async Task<bool> AddProductAsync(ProductDTO productDTO ,Guid userid)
		{
			byte[] Image = Encoding.UTF8.GetBytes(productDTO.ProductImage);
			
			var product = new Product
			{
				Id = Guid.NewGuid(),
				ProductCode = productDTO.ProductCode,
				ProductName = productDTO.ProductName,
				HSNCode = productDTO.HSNCode,
				TotalStock = productDTO.TotalStock,
				ProductImage = Image,
				CategoryId = productDTO.CategoryId,
				UserId = userid,
				IsFavourite = false,
				Active = true
			};

			 await _repository.AddProductAsync(product);
			
			if (productDTO.Variants == null)
{
    // Log or breakpoint here
    throw new Exception("Variants list is null");
}
if (!productDTO.Variants.Any())
{
    // Log or breakpoint here
    throw new Exception("Variants list is empty");
}

			foreach (var variantDTO in productDTO.Variants)
			{
				var variant = new ProductVariant
				{
					Id = Guid.NewGuid(),
					Name = variantDTO.Name,
					ProductId = product.Id
				};
				await _varientRepository.AddVarientAsync(variant);

				foreach (var subVariantDTO in variantDTO.SubVariants)
				{
					var subVariant = new ProductSubVariant
					{
						Id = Guid.NewGuid(),
						OptionValue = subVariantDTO.OptionValue,
						ProductVariantId = variant.Id
					};

					
					await _varientRepository.AddSubVarient(subVariant);
				}


				
			}
			return true;

		}

	}
}
