using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.Services
{
	public interface IOrderServices
	{
		public Task<List<Order>> GetAllOrder();
		public Task<Order> GetOrderById(int id);
		public Task<bool> AddOrder(AddOrderDto addOrderDto);
		public Task<bool> DeleteOrder(int id);
	}
}
