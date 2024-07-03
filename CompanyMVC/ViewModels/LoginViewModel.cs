using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password Is Required")]
		public string Password { get; set; }
		[Required]
		public bool RememberMe { get; set; }
	}
}
