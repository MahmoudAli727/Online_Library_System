using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Online_Lbrary_System.Data.Model
{
	public class Order
	{
		public int Id { get; set; }
		[ForeignKey(nameof(Users))]
		public string UserId { get; set; }
		[ForeignKey(nameof(book))]
		public int BookId { get; set; }
		public DateTime OrderDate { get; set; }
		public bool Returned { get; set; }
		public DateTime? ReturnDate { get; set; }
		[IgnoreDataMember]
		[JsonIgnore]
		public virtual AppUser? Users { get; set; }
		[IgnoreDataMember]
		[JsonIgnore]
		public virtual Book? book { get; set; }
	}
}
