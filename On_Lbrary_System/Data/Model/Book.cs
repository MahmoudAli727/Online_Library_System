using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Online_Lbrary_System.Data.Model
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public string Isbn { get; set; } = string.Empty;
		public int Rack { get; set; }
		public float Price { get; set; }
		public bool Ordered { get; set; }

		[ForeignKey(nameof(BookCategory))]
		public int BookCategoryId { get; set; }
		[IgnoreDataMember]
		[JsonIgnore]
		public virtual BookCategory? BookCategory { get; set; }
	}
}
