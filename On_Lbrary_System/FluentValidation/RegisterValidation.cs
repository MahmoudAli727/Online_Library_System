using FluentValidation;
using Online_Lbrary_System.Data.Dtos;

namespace Online_Lbrary_System.FluentValidation
{
	public class RegisterValidation : AbstractValidator<RegisterDto>
	{
		public RegisterValidation()
		{
			RuleFor(user => user.FName).NotNull().NotEmpty().Length(2, 255).Must(isValidName).When(user => user.FName != null);
			RuleFor(user => user.LName).NotNull().NotEmpty().Length(2, 255).Must(isValidName).When(user => user.LName != null);
			RuleFor(user => user.UserName).NotNull().NotEmpty().Length(2, 255);
			RuleFor(user => user.Email).NotNull().NotEmpty().EmailAddress();
			RuleFor(user => user.Password).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30);
			RuleFor(user => user.ConfirmedPassword).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30)
				.Must((user, ConPass) => ConPass == user.Password).WithMessage("ConfirmedPassword is not equal Password");
			RuleFor(user => user.phone).NotNull().NotEmpty().Must(isValidPhone).When(uPhone => uPhone != null);

		}

		private bool isValidPhone(string arg)
		{
			return arg.All(char.IsDigit);
		}

		private bool isValidName(string arg)
		{
			return arg.All(char.IsLetter);
		}
	}
}
