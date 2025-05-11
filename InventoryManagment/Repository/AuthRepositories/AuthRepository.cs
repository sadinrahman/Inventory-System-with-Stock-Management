using InventoryManagment.Data;
using InventoryManagment.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagment.Repository.AuthRepositories
{
	public class AuthRepository: IAuthRepository
	{
		private readonly AppDbContext _context;
		public AuthRepository(AppDbContext context)
		{
			_context = context;
		}
		public async Task<bool> AddUser(User user)
		{
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return true;
		}
		public async Task<User> GetUser(string email)
		{
			var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
			if (user == null) return null;
			Console.WriteLine(user.Username);
			return user;
		}
	}
}
