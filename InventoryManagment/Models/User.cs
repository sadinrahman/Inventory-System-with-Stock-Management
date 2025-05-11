using System.ComponentModel.DataAnnotations;

namespace InventoryManagment.Models
{
	public class User
	{
		[Key]
		public Guid Id { get; set; }
		[Required]
		[StringLength(100)]
		public string Username { get; set; }
		[Required]
		[StringLength(100)]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public List<Product> Products { get; set; } 
	}
}
