using AutoMapper;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.AutoMapper
{
	public class BookCategoryMapper:Profile
	{
        public BookCategoryMapper()
        {
            CreateMap<BookCategory, BookCategoryDto>().
                ReverseMap();
        }
    }
}
