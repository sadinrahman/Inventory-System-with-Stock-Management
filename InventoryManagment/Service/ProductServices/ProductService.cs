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
		public async Task<List<ProductViewDto>> GetAllAsync()
		{
			var res=await _repository.GetAllAsync();
			var productViewDtos = new List<ProductViewDto>();
			foreach (var product in res)
			{
				foreach (var variant in product.Variants)
				{
					foreach (var subVariant in variant.SubVariants)
					{
						var dto = new ProductViewDto
						{
							ProductCode = product.ProductCode,
							ProductName = product.ProductName,
							CreatedDate = product.CreatedDate, 
							HSNCode = product.HSNCode,
							TotalStock = product.TotalStock,
							categoryName = product.Category?.Name,
							UserName = product.User?.Username,
							VariantName = variant.Name,
							SubVariantName = subVariant.OptionValue
						};
						productViewDtos.Add(dto);
					}
				}
			}

			return productViewDtos;
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
		public async Task<string> AddStockAsync(ProductStockUpdateDto productStock,Guid userid)
		{
			var product = await _repository.GetProductById(productStock.ProductId);
			if (product == null || product.ProductCode != productStock.ProductCode)
			{
				return "Product not found";
			}
			var user = product.User.Id;
			if (user != userid)
			{
				return "You are not authorized to update this product";
			}
			var variant = product.Variants.FirstOrDefault(v => v.Name == productStock.VariantName);
			if (variant == null)
			{
				return "Variant not found";
			}
			var subVariant = variant.SubVariants.FirstOrDefault(sv => sv.OptionValue == productStock.SubVariantName);
			if (subVariant == null)
			{
				return "Sub-variant not found";
			}
			product.TotalStock += productStock.Stock;
			await _repository.UpdateProductAsync(product);
			return "Stock updated successfully";
		}
		public async Task<string> RemoveStockAsync(ProductStockUpdateDto productStock)
		{
			var product = await _repository.GetProductById(productStock.ProductId);
			if (product == null || product.ProductCode != productStock.ProductCode)
			{
				return "Product not found";
			}
			
			var variant = product.Variants.FirstOrDefault(v => v.Name == productStock.VariantName);
			if (variant == null)
			{
				return "Variant not found";
			}
			var subVariant = variant.SubVariants.FirstOrDefault(sv => sv.OptionValue == productStock.SubVariantName);
			if (subVariant == null)
			{
				return "Sub-variant not found";
			}
			product.TotalStock -= productStock.Stock;
			await _repository.UpdateProductAsync(product);
			return "Stock updated successfully";
		}

	}
}
