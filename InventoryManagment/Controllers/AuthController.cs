using InventoryManagment.Dto;
using InventoryManagment.Service.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

		private readonly IAuthService _authService;
		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDto register)
		{
			
			var result =await _authService.Register(register);
			if (result == "Email is already registered")
			{
				return BadRequest(result);
			}else if(result == "Registration successful")
			{
				return Ok(result);
			}
			return BadRequest("An error occurred during registration");
		}
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDto login)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var token = await _authService.Login(login);

			if (token == "Invalid username or password")
				return Unauthorized(token);

			
			Response.Cookies.Append("AuthToken", token, new CookieOptions
			{
				HttpOnly = true,
				Secure = true, 
				SameSite = SameSiteMode.Strict,
				Expires = DateTimeOffset.UtcNow.AddHours(5)
			});

			return Ok(new { Token = token }); 
		}
	}
}
