using Microsoft.AspNetCore.Identity;

namespace DAL.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string FName { get; set; }
		public string LName { get; set; }
		public bool ISAgree { get; set; }
	}
}
