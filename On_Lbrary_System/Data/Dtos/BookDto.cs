using Online_Lbrary_System.Data.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Lbrary_System.Data.Dtos
{
	public class BookDto
	{
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public string Isbn { get; set; } = string.Empty;
		public int Rack { get; set; }
		public float Price { get; set; }
		public bool Ordered { get; set; }
		public int BookCategoryId { get; set; }
	}
}
