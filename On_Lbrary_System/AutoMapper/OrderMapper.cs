using AutoMapper;
using Online_Lbrary_System.Data.Dtos;
using Online_Lbrary_System.Data.Model;

namespace Online_Lbrary_System.AutoMapper
{
	public class OrderMapper:Profile
	{
        public OrderMapper()
        {
            CreateMap<Order, OrderDto>()
                .ReverseMap();
        }
    }
}
