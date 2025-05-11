using InventoryManagment.Dto;
using InventoryManagment.Models;
using InventoryManagment.Repository.AuthRepositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InventoryManagment.Service.AuthServices
{
	public class AuthService: IAuthService
	{
		private readonly IAuthRepository _repo;
		private readonly IConfiguration _config;
		public AuthService(IAuthRepository authRepository, IConfiguration config)
		{
			_repo = authRepository;
			_config = config;
		}
		public async Task<string> Login(LoginDto login)
		{
			var user =await _repo.GetUser(login.Email);

			if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
			{
				return "Invalid username or password";
				
			}
			string token = GenerateToken(user);

			return token;
		}
		public async Task<string> Register(RegisterDto register)
		{
			try
			{
				var existingUser =await _repo.GetUser(register.Email);
				if (existingUser != null)
				{
					return "Email is already registered";
				}

				register.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);
				var user = new User
				{
					Username = register.Username,
					Email = register.Email,
					Password = register.Password
				};

				await _repo.AddUser(user);

				return "Registration successful";
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred during registration", ex);
			}
		}




		private string GenerateToken(User user)
		{
			var claims = new[]
			{
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddHours(1),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
