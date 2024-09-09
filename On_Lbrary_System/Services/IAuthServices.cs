using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Services
{
	public interface IAuthServices
	{
		Task<Auth> RegisterAsync(RegisterDto model);
		Task<Auth> LoginAsync(LoginDto model);
		Task<List<AppUser>> GetUsers();
		//Task<bool> UpdateUserAsync(string email, RegisterDto model);
		Task<bool> DeleteUserAsync(string email);
	}
}
