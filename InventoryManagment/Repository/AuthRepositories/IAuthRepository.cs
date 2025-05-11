using InventoryManagment.Models;

namespace InventoryManagment.Repository.AuthRepositories
{
	public interface IAuthRepository
	{
		Task<User> GetUser(string email);
		Task<bool> AddUser(User user);
	}
}
