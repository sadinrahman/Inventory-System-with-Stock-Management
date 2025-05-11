using System.ComponentModel.DataAnnotations;

namespace InventoryManagment.Models
{
	public class Product
	{
		public Guid Id { get; set; }
		public string ProductCode { get; set; }
		public string ProductName { get; set; }
		public byte[] ProductImage { get; set; }
		public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
		public DateTimeOffset UpdatedDate { get; set; }=DateTimeOffset.UtcNow;
		public Guid UserId { get; set; }
		public bool IsFavourite { get; set; }
		public bool Active { get; set; } = true;
		[MaxLength(100)]
		public string HSNCode { get; set; }
		public decimal TotalStock { get; set; }
		public Guid CategoryId { get; set; }

		public ICollection<ProductVariant> Variants { get; set; }= new List<ProductVariant>();
		public Category Category { get; set; }
		public User User { get; set; }
	}
}
