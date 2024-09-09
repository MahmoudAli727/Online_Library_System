using Microsoft.EntityFrameworkCore;
using Online_Lbrary_System.Data;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Services
{
	public class BookCategoryServices : IBookCategoryServices
	{

		private readonly AppDbContext _appDbContext;
		public BookCategoryServices(AppDbContext appContext)
		{
			this._appDbContext = appContext;
		}		

		public async Task<List<BookCategory>> GetAllBookCategory()
		{
			var bookCategories = await _appDbContext.bookCategories.ToListAsync();
			return bookCategories;
		}

		public async Task<BookCategory> GetBookCategoryById(int id)
		{
			var bookCategory = await _appDbContext.bookCategories.FirstOrDefaultAsync(x => x.Id == id);
			if (bookCategory == null)
			{
				return null;
			}
			return bookCategory;
		}

		public async Task<BookCategory> AddBookCategory(BookCategoryDto bookCategoryDto)
		{
			var bookCategory= new BookCategory() { Category=bookCategoryDto.Category };
			await _appDbContext.bookCategories.AddAsync(bookCategory);
			await _appDbContext.SaveChangesAsync();
			return bookCategory;
		}

		public async Task<bool> UpdateBookCategory(int id, BookCategoryDto bookCategoryDto)
		{
			var bookCategory = await _appDbContext.bookCategories.FirstOrDefaultAsync(x => x.Id == id);
			if (bookCategory == null)
			{
				return false;
			}
			bookCategory.Category = bookCategoryDto.Category;
			_appDbContext.Update(bookCategory);
			await _appDbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteBookCategoryById(int id)
		{
			var bookCategory = await _appDbContext.bookCategories.FindAsync(id);

			if (bookCategory == null)
				return false;

			_appDbContext.bookCategories.Remove(bookCategory);
			await _appDbContext.SaveChangesAsync();
			return true;
		}
		public async Task<bool> DeleteBookCategoryByTitle(string title)
		{
			var bookCategory = await _appDbContext.bookCategories.FirstOrDefaultAsync(x => x.Category == title);
			if (bookCategory == null)
				return false;
			_appDbContext.Remove(bookCategory);
			await _appDbContext.SaveChangesAsync();
			return true;
		}

	}
}
