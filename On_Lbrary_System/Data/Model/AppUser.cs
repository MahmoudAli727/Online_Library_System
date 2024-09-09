using Microsoft.AspNetCore.Identity;

namespace Online_Lbrary_System.Data.Model
{
	public class AppUser: IdentityUser
	{
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string phone { get; set; }
		public int Age { get; set; }
		public byte[]? image { get; set; }
	}
}
