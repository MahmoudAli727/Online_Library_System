using FluentValidation;
using Online_Lbrary_System.Data.Dtos;

namespace Online_Lbrary_System.FluentValidation
{
	public class BookDtoValidation:AbstractValidator<BookDto>
	{
        public BookDtoValidation()
        {
            RuleFor(book=>book.Title).NotEmpty().NotNull().MinimumLength(3).MaximumLength(20);
            RuleFor(book=>book.Author).NotEmpty().NotNull().MinimumLength(3).MaximumLength(20).Must(Titleisvalidat).When(b => b.Author != null);
            RuleFor(book=>book.Rack).NotEmpty().NotNull().Must(isRackValid);
            RuleFor(book=>book.Isbn).NotEmpty().NotNull().MinimumLength(3).MaximumLength(20).Must(ISBNIsvalidat).When(b => b.Isbn != null);
            RuleFor(book=>book.BookCategoryId).NotEmpty().NotNull();
            RuleFor(book=>book.Price).NotEmpty().NotNull();
        }

		private bool isRackValid(int arg)
		{
			return arg > 0 && arg <= 100;
		}

		private bool ISBNIsvalidat(string arg)
		{
			return arg.All(char.IsDigit);
		}

		private bool Titleisvalidat(string arg)
		{
			return arg.All(char.IsLetter);
		}
	}
}
