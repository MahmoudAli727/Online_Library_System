using AutoMapper;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.AutoMapper
{
	public class userViewDto : Profile
	{
		public userViewDto()
		{
			CreateMap<AppUser, UserDto>().
			ForMember(user => user.FName, src => src.MapFrom(src => src.firstName)).
			ForMember(user => user.LName, src => src.MapFrom(src => src.lastName));
		}
	}
}
