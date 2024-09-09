using FluentValidation;
using Online_Lbrary_System.Data.Dtos;

namespace Online_Lbrary_System.FluentValidation
{
	public class bookCategoryDtoValidation:AbstractValidator<BookCategoryDto>
	{
        public bookCategoryDtoValidation()
        {
			RuleFor(cat => cat.Category).NotEmpty().NotNull().MinimumLength(2).MaximumLength(10).Must(isValidCat).When(cat => cat.Category != null);
        }

		private bool isValidCat(string arg)
		{
				return arg.All(char.IsLetter);
		}
	}
}
