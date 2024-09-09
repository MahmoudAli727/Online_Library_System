using FluentValidation;
using Online_Lbrary_System.Data.Dtos;

namespace Online_Lbrary_System.FluentValidation
{
	public class OrderDtoValidation:AbstractValidator<AddOrderDto>
	{
        public OrderDtoValidation()
        {
            RuleFor(order=>order.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(order=>order.BookId).NotNull().NotEmpty();
        }
    }
}