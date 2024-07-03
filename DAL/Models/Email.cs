namespace DAL.Models
{
	public class Email
	{
		public int Id { get; set; }//to use if we create this table in database
		public string To { get; set; }
		public string Subject { get; set; }
		public string Body { get; set; }
	}
}
