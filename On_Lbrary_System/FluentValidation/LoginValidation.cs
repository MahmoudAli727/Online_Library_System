using FluentValidation;
using Online_Lbrary_System.Data.Dtos;

namespace Online_Lbrary_System.FluentValidation
{
	public class LoginValidation : AbstractValidator<LoginDto>
	{
		public LoginValidation()
		{
			RuleFor(user => user.Email).NotEmpty().NotNull().EmailAddress().When(user => user.Email != null);
			RuleFor(user => user.Password).NotNull().NotEmpty().MinimumLength(5);
		}
	}
}
