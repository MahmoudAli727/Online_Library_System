namespace Online_Lbrary_System.Data.Dtos
{
	public class OrderDto
	{

		//public int UserId { get; set; }
		public string Email { get; set; }
		public int BookId {  get; set; }
		public DateTime OrderDate { get; set; } = DateTime.Now;
		public bool Returned { get; set; }=false;
		public DateTime? ReturnDate { get; set; }
	}
}
