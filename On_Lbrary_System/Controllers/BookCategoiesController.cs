using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;
using Online_Lbrary_System.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace Online_Lbrary_System.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookCategoiesController : ControllerBase
	{
		private readonly IBookCategoryServices _bookCategoryServices;
		private readonly IMapper _mapper;

		public BookCategoiesController(IBookCategoryServices bookCategoryServices, IMapper mapper)
		{
			_bookCategoryServices = bookCategoryServices;
			_mapper = mapper;
		}

		[Authorize()]
		[HttpGet("GetAllCategories")]
		public async Task<IActionResult> GetAllCategories()
		{
			try
			{
				var bookCategories=await _bookCategoryServices.GetAllBookCategory();
				if (bookCategories ==null)
					return NotFound("Empty");
				var bookCategoriesDto = new List<BookCategoryDto>();

				foreach (var category in bookCategories)
				{
					bookCategoriesDto.Add(_mapper.Map<BookCategoryDto>(category));
				}
				return Ok(bookCategoriesDto);
            }
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize()]
		[HttpGet("GetBookCategoryById")]
		public async Task<IActionResult> GetBookCategoryById(int id)
		{
			try
			{
				var bookCategory = await _bookCategoryServices.GetBookCategoryById(id);
				if (bookCategory == null)
					return NotFound($"This Id {id} not exist");
				var bookCategoryDto=_mapper.Map<BookCategoryDto>(bookCategory);

				return Ok(bookCategoryDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "Librarian")]
		[HttpPost("AddBookCategory")]
		public async Task<IActionResult> AddBookCategory([FromForm] BookCategoryDto bookCategoryDto)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var newBook = await _bookCategoryServices.AddBookCategory(bookCategoryDto);
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
		[HttpPut("UpdateBookCategory")]
		public async Task<IActionResult> UpdateBookCategory(int id, [FromForm] BookCategoryDto bookCategoryDto)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var newBook = await _bookCategoryServices.UpdateBookCategory(id,bookCategoryDto);
                    if (newBook)
                       return Ok("updated Successfully");
					return NotFound($"This Id {id} not exist");
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
		[HttpDelete("DeleteBookCategoryById")]
		public async Task<IActionResult> DeleteBookCategoryById(int id)
		{
			try
			{
				var bookCat = await _bookCategoryServices.DeleteBookCategoryById(id);
				if (bookCat == false)
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
		[HttpDelete("DeleteBookCategoryByTitle")]
		public async Task<IActionResult> DeleteBookCategoryByTitle(string title)
		{
			try
			{
				var bookCat = await _bookCategoryServices.DeleteBookCategoryByTitle(title);
				if (bookCat == false)
				{
					return NotFound($"this title '{title}' is not exist");
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
