namespace InventoryManagment.Dto
{
	public class ProductVariantDTO
	{
		public string Name { get; set; }
		public ICollection<ProductSubVariantDTO> SubVariants { get; set; }
	}
}
