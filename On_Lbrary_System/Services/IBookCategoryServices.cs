using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Services
{
	public interface IBookCategoryServices
	{
		public Task<List<BookCategory>> GetAllBookCategory();
		public Task<BookCategory> GetBookCategoryById(int id);
		public Task<BookCategory> AddBookCategory(BookCategoryDto bookCategoryDto);
		public Task<bool> UpdateBookCategory(int id, BookCategoryDto bookCategoryDto);
		public Task<bool> DeleteBookCategoryById(int id);
		public Task<bool> DeleteBookCategoryByTitle(string title);
	}
}
