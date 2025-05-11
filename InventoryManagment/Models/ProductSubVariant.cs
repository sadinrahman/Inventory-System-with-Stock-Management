﻿namespace InventoryManagment.Models
{
	public class ProductSubVariant
	{
		public Guid Id { get; set; }
		public string OptionValue { get; set; }

		public Guid ProductVariantId { get; set; }
		public ProductVariant ProductVariant { get; set; }

	}
}
