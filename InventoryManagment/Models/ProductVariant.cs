﻿namespace InventoryManagment.Models
{
	public class ProductVariant
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public Guid ProductId { get; set; }
		public Product Product { get; set; }

		public ICollection<ProductSubVariant> SubVariants { get; set; } = new List<ProductSubVariant>();
	}
}
