using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Services;

namespace Online_Lbrary_System.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		private readonly IBookServices _bookServices;
		private readonly IMapper _mapper;

		public BooksController(IBookServices bookServices, IMapper mapper)
		{
			_bookServices = bookServices;
			_mapper = mapper;
		}

		[Authorize()]
		[HttpGet("getAllBooks")]
		public async Task<IActionResult> GetAllBooks()
		{
			try
			{
				var books = await _bookServices.GetAllBook();
				if (books == null)
					return NotFound("Empty");
				var booksDto = new List<BookDto>();

				foreach (var book in books)
				{
					booksDto.Add(_mapper.Map<BookDto>(book));
				}
				return Ok(booksDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize()]
		[HttpGet("GetBookById")]
		public async Task<IActionResult> GetBookById(int id)
		{
			try
			{
				var book = await _bookServices.GetBookById(id);
				if (book == null)
				{
					return NotFound($"this id {id} is not exist");
				}
				var bookDto = _mapper.Map<BookDto>(book);
				return Ok(bookDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize()]
		[HttpGet("GetBookByName")]
		public async Task<IActionResult> GetBookyName(string name)
		{
			try
			{
				var book = await _bookServices.GetBookByName(name);
				if (book == null)
				{
					return NotFound($"this name {name} is not exist");
				}
				var bookDto = _mapper.Map<BookDto>(book);
				return Ok(bookDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize()]
		[HttpGet("GetBooksByRack")]
		public async Task<IActionResult> GetBooksByRack(int rack)
		{
			try
			{
				var books = await _bookServices.GetBookByRack(rack);
				if (books == null)
				{
					return NotFound($"this {rack} is Empty");
				}
				var booksDto = new List<BookDto>();

				foreach (var book in books)
				{
					booksDto.Add(_mapper.Map<BookDto>(book));
				}
				return Ok(booksDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize()]
		[HttpGet("GetBookByIsbn")]
		public async Task<IActionResult> GetBookByIsbn(string isbn)
		{
			try
			{
				var book = await _bookServices.GetBookByIsbn(isbn);
				if (book == null)
				{
					return NotFound($"this isbn {isbn} is not exist");
				}
				var bookDto = _mapper.Map<BookDto>(book);
				return Ok(bookDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize()]
		[HttpGet("GetBookByNotOrdered")]
		public async Task<IActionResult> GetBookByNotOrdered(bool ordered)
		{
			try
			{
				var books = await _bookServices.GetBookByNotOrdered(ordered);
				if (books == null)
				{
					return NotFound("Empty");
				}
				var booksDto = new List<BookDto>();

				foreach (var book in books)
				{
					booksDto.Add(_mapper.Map<BookDto>(book));
				}
				return Ok(booksDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "Librarian")]
		[HttpPost("AddBook")]
		public async Task<IActionResult> AddBook([FromForm] BookDto bookDto)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var book = await _bookServices.AddBook(bookDto);
					if (book == null)
						return NotFound($"Category id {bookDto.BookCategoryId} is not exist Or Isbn {bookDto.Isbn} is Already exist");

					return Ok("Added Successfully");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

		[Authorize(Roles = "Librarian")]
		[HttpPut("UpdateBook")]
		public async Task<IActionResult> UpdateBook(int id, [FromForm] BookDto bookDto)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var newBook = await _bookServices.UpdateBook(id, bookDto);
					if (newBook == false)
						return NotFound($"this id {id} is not exist Or bookCategoryId {bookDto.BookCategoryId} is not exist");
					return Ok("Updated Successfully");
				}
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
			else
				return BadRequest("someThing is wrong");
		}

		[Authorize(Roles = "Librarian")]
		[HttpDelete("DeleteBookById")]
		public async Task<IActionResult> DeleteBookById(int id)
		{
			try
			{
				var isDeleted = await _bookServices.DeleteBookById(id);
				if (isDeleted == false)
				{
					return NotFound($"this id {id} is not exist");
				}
				return Ok("Deleted Successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "Librarian")]
		[HttpDelete("DeleteBookByName")]
		public async Task<IActionResult> DeleteCardByName(string name)
		{
			try
			{
				var isDeleted = await _bookServices.DeleteBookByTitle(name);
				if (isDeleted == false)
				{
					return NotFound($"this Name{name} is not exist");
				}
				return Ok("Deleted Successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}