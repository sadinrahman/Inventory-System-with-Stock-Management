namespace InventoryManagment.Dto
{
	public class ProductStockUpdateDto
	{
		public Guid ProductId { get; set; }
		public string ProductCode { get; set; }
		public string VariantName { get; set; }
		public string SubVariantName { get; set; }
		public int Stock { get; set; }
	}
}
