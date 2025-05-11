namespace InventoryManagment.Dto
{
	public class ProductViewDto
	{
		public string ProductCode { get; set; }
		public string ProductName { get; set; }
		public DateTimeOffset CreatedDate { get; set; }
		public string HSNCode { get; set; }
		public decimal TotalStock { get; set; }
		public string categoryName { get; set; }
		public string UserName { get; set; }
		public string VariantName { get; set; }
		public string SubVariantName { get; set; }
	}
}
