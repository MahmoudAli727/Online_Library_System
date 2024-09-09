using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Services
{
	public interface IBookServices
	{
		public Task<List<Book>> GetAllBook();
		public Task<Book> GetBookById(int id);
		public Task<Book> GetBookByName(string title);
		public Task<List<Book>> GetBookByRack(int rack);
		public Task<Book> GetBookByIsbn(string isbn);
		public Task<List<Book>> GetBookByNotOrdered(bool isNotOrdered);
		public Task<Book> AddBook(BookDto bookDto);
		public Task<bool> UpdateBook(int id, BookDto bookDto);
		public Task<bool> DeleteBookById(int id);
		public Task<bool> DeleteBookByTitle(string title);
	}
}
