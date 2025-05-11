using InventoryManagment.Dto;
using InventoryManagment.Models;
using InventoryManagment.Service.ProductServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
		private readonly IProductService _productService;
		public ProductController(IProductService productService)
		{
			_productService = productService;
		}
		
		[HttpPost]
		[Authorize]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO product)
		{
			if (product == null)
			{
				return BadRequest("Product data is required.");
			}
			
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userid))
			{
				var res = await _productService.AddProductAsync(product,userid);
				return Ok("Product added successfully.");
			}
			return BadRequest("user not valid");
		}
	}
}
