using InventoryManagment.Dto;

namespace InventoryManagment.Service.AuthServices
{
	public interface IAuthService
	{
		Task<string> Login(LoginDto login);
		Task<string> Register(RegisterDto register);
	}
}
