using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Online_Lbrary_System.Data;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Services
{
	public class OrderServices : IOrderServices
	{
		private readonly AppDbContext _appDbContext;
		private readonly IBookServices _bookServices;
		private readonly UserManager<AppUser> _userManager;

		public OrderServices(AppDbContext appDbContext, UserManager<AppUser> userManager, IBookServices bookServices)
		{
			_appDbContext = appDbContext;
			_userManager = userManager;
			_bookServices = bookServices;
		}

		public async Task<List<Order>> GetAllOrder()
		{
			var orders=await _appDbContext.orders.ToListAsync();
			return orders;
		}
		
		public async Task<Order> GetOrderById(int id)
		{
			var order = await _appDbContext.orders.FindAsync(id);
			if (order == null)
				return null;

			return order;
		}

		public async Task<bool> AddOrder(AddOrderDto addOrderDto)
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == addOrderDto.Email);
            if (user ==null)
				return false;

			var book = await _bookServices.GetBookById(addOrderDto.BookId);
			if (book == null)
				return false;

			var order = new Order()
			{
				UserId = user.Id,
				BookId = addOrderDto.BookId,
				OrderDate = DateTime.Now,
				ReturnDate = DateTime.Now.AddDays(10),
				Returned = false
			};
			await _appDbContext.orders.AddAsync(order);
			await _appDbContext.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteOrder(int id)
		{

			var order = await _appDbContext.orders.FindAsync(id);
			if (order == null)
				return false;

			_appDbContext.Remove(order);
			await _appDbContext.SaveChangesAsync();
			return true;
		}

	}
}
