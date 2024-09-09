using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;
using Online_Lbrary_System.Services;

namespace Online_Lbrary_System.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderServices _orderServices;

		public OrdersController(IOrderServices orderServices)
		{
			_orderServices = orderServices;
		}

		[Authorize(Roles = "Librarian")]
		[HttpGet("getAllOrders")]
		public async Task<IActionResult> GetAllOrders()
		{
			try
			{
				var orders = await _orderServices.GetAllOrder();
				if (orders == null)
					return NotFound("Empty");
		
				return Ok(orders);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles ="User")]
		[HttpGet("GetOrderById")]
		public async Task<IActionResult> GetOrderById(int id)
		{
			try
			{
				var order = await _orderServices.GetOrderById(id);
				if (order== null)
				{
					return NotFound($"this id {id} is not exist");
				}

				//var bookDto = _mapper.Map<BookDto>(book);
				return Ok(order);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[Authorize(Roles = "User")]
		[HttpPost("AddOrder")]
		public async Task<IActionResult> AddOrder([FromForm] AddOrderDto addOrderDto)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var orderAdded = await _orderServices.AddOrder(addOrderDto);
					if (!orderAdded)
						return BadRequest($"Book id {addOrderDto.BookId} Or Email {addOrderDto.Email} is not correct");

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

		[Authorize(Roles = "User")]
		[HttpDelete("DeleteOrderById")]
		public async Task<IActionResult> DeleteOrderById(int id)
		{
			try
			{
				var isDeleted = await _orderServices.DeleteOrder(id);
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
	}
}