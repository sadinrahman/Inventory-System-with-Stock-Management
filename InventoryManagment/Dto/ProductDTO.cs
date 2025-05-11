namespace InventoryManagment.Dto
{
	public class ProductDTO
	{
		public string ProductCode { get; set; }
		public string ProductName { get; set; }
		public string ProductImage { get; set; }
		public string HSNCode { get; set; }
		public decimal TotalStock { get; set; }
		public Guid CategoryId { get; set; }
		public ICollection<ProductVariantDTO> Variants { get; set; }
	}
}
