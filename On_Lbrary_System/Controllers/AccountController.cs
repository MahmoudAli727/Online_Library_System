using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Services;

namespace Online_Lbrary_System.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAuthServices _authServices;
		private readonly IMapper _mapper;

		public AccountsController(IAuthServices authServices, IMapper mapper)
		{
			_authServices = authServices;
			_mapper = mapper;
		}

		[HttpPost("Register")]
		public async Task<IActionResult> RegisterAsync([FromForm] RegisterDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authServices.RegisterAsync(model);

			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok(result);
		}

		[HttpPost("Login")]
		public async Task<IActionResult> LoginAsync([FromForm] LoginDto model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authServices.LoginAsync(model);

			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok(result);
		}

		[Authorize()]
		[HttpGet("GettAllUsers")]
		public async Task<IActionResult> GetAllUser()
		{
			try
			{
				var users = await _authServices.GetUsers();
				if (users == null)
					return NotFound("Empty");
				var usersDto = new List<UserDto>();
				foreach (var user in users)
				{
					usersDto.Add(_mapper.Map<UserDto>(user));
				}

				return Ok(usersDto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//[Authorize(Roles = "Librarian")]
		//[HttpPut("UpdateUser")]
		//public async Task<IActionResult> UpdateStudent(string email, [FromForm] RegisterDto registerDto)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		try
		//		{

		//			var Edit = await _authServices.UpdateUserAsync(email, registerDto);
		//			if (Edit == false)
		//				return NotFound($"this id {email} is not exist");
		//			return Ok("Updated Successfully");
		//		}
		//		catch (Exception ex)
		//		{
		//			return BadRequest(ex.Message);
		//		}
		//	}
		//	else
		//		return BadRequest("someThing is wrong");
		//}

		[Authorize(Roles = "Librarian")]
		[HttpDelete("DeleteUser")]
		public async Task<IActionResult> DeleteUser(string email)
		{
			try
			{
				var Del = await _authServices.DeleteUserAsync(email);
				if (Del == false)
					return NotFound("Email is not exist");

				return Ok("Deleted Successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

	}
}
