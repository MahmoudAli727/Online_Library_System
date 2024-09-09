using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Online_Lbrary_System.Data.Model
{
	public class BookCategory
	{
		public int Id { get; set; }
		public string Category { get; set; } = string.Empty;
		[IgnoreDataMember]
		[JsonIgnore]
		public virtual ICollection<Book> books { get; set; }
	}
}
