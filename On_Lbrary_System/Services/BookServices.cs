using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Lbrary_System.Data;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Services
{
	public class BookServices : IBookServices
	{
		private readonly IMapper _mapper;
		private readonly AppDbContext _appDbContext;
		private readonly IBookCategoryServices _bookCategoryServices;


		public BookServices(IMapper mapper, AppDbContext appDbContext, IBookCategoryServices bookCategoryServices)
		{
			_mapper = mapper;
			_appDbContext = appDbContext;
			_bookCategoryServices = bookCategoryServices;
		}

		public async Task<List<Book>> GetAllBook()
		{
			var books = await _appDbContext.books.ToListAsync();
			if (books == null)
				return null;
			return books;
		}

		public async Task<Book> GetBookById(int id)
		{
			var book = await _appDbContext.books.FindAsync(id);
			if (book == null)
				return null;
			return book;
		}

		public async Task<Book> GetBookByIsbn(string isbn)
		{
			var book = await _appDbContext.books.FirstOrDefaultAsync(x=>x.Isbn==isbn);
			if (book == null)
				return null;
			return book;
		}

		public async Task<Book> GetBookByName(string title)
		{
			var book = await _appDbContext.books.FirstOrDefaultAsync(x => x.Title == title);
			if (book == null)
				return null;
			return book;
		}

		public async Task<List<Book>> GetBookByNotOrdered(bool isNotOrdered)
		{
			var books = await _appDbContext.books.Where(x => x.Ordered == isNotOrdered).ToListAsync();
			if (books == null)
				return null;
			return books;
		}

		public async Task<List<Book>> GetBookByRack(int rack)
		{
			var books = await _appDbContext.books.Where(x=>x.Rack==rack).ToListAsync();
			if (books == null)
				return null;
			return books;
		}

		public async Task<Book> AddBook(BookDto bookDto)
		{
			var bookCategory = await _bookCategoryServices.GetBookCategoryById(bookDto.BookCategoryId);
				//await _appDbContext.bookCategories.FirstOrDefaultAsync(x => x.Id == bookDto.BookCategoryId);
			if (bookCategory == null)
				return null;
			var isbnFounded = await _appDbContext.books.FirstOrDefaultAsync(x => x.Isbn == bookDto.Isbn);
            if (isbnFounded !=null)
            {
				return null;
            }

            var book =_mapper.Map<Book>(bookDto);
			await _appDbContext.books.AddAsync(book);
			await _appDbContext.SaveChangesAsync();
			return book;
		}

		public async Task<bool> UpdateBook(int id, BookDto bookDto)
		{
			var book = await _appDbContext.books.FindAsync(id);
			var bookCategory = await _bookCategoryServices.GetBookCategoryById(bookDto.BookCategoryId);

			if (book == null)
				return false;

			if (bookCategory == null)
				return false;
			book.Title = bookDto.Title;
			book.Author = bookDto.Author;
			book.Price = bookDto.Price;
			book.Ordered = bookDto.Ordered;
			book.Rack = bookDto.Rack;
			book.BookCategoryId = bookCategory.Id;
			book.Isbn = bookDto.Isbn;

			//book=_mapper.Map<Book>(bookDto);

			_appDbContext.books.Update(book);
			await _appDbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteBookById(int id)
		{
			var book = await _appDbContext.books.FindAsync(id);

			if (book == null)
				return false;

			_appDbContext.books.Remove(book);
			await _appDbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteBookByTitle(string title)
		{
			var book = await _appDbContext.books.FirstOrDefaultAsync(x => x.Title == title);

			if (book == null)
				return false;

			_appDbContext.books.Remove(book);
			await _appDbContext.SaveChangesAsync();
			return true;
		}
	}
}
