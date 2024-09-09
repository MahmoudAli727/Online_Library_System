using AutoMapper;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.AutoMapper
{
	public class UserMapper : Profile
	{
		public UserMapper()
		{
			CreateMap<AppUser, RegisterDto>().
				ForMember(user => user.FName, src => src.MapFrom(src => src.firstName)).
				ForMember(user => user.LName, src => src.MapFrom(src => src.lastName)).
				ForMember(src => src.image, opt => opt.Ignore());
		}
	}
}
