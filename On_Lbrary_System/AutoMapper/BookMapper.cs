using AutoMapper;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.AutoMapper
{
	public class BookMapper:Profile
	{
        public BookMapper()
        {
			CreateMap<BookDto, Book>()
				.ReverseMap();
		}
    }
}
