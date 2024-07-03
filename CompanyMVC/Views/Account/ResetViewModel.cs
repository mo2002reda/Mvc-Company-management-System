using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.Views.Account
{
	public class ResetViewModel
	{
		[Required]
		[DataType(DataType.Password, ErrorMessage = "Password Is Required")]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password, ErrorMessage = "Confirm New Password Is Required")]
		[Compare("Password", ErrorMessage = "Password Miss Match")]
		public string ConfirmPassword { get; set; }
	}
}