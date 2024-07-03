using System.ComponentModel.DataAnnotations;

namespace CompanyMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Firt Name Is Required")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Last Name Is Required")]
        public string LName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Missed Match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool IsAgree { get; set; }
    }
}
