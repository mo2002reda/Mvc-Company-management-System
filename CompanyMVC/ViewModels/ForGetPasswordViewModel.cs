using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.ViewModels
{
	public class ForGetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
