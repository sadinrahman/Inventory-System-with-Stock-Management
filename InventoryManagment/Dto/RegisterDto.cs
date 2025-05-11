using System.ComponentModel.DataAnnotations;

namespace InventoryManagment.Dto
{
	public class RegisterDto
	{
		[Required]
		[StringLength(100)]
		public string Username { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[MinLength(6, ErrorMessage = "Password must be above 6 characters")]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
		ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]

		public string Password { get; set; }
	}
}
