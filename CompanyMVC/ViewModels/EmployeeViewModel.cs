using DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;
//this class as a merrior for table in the view(front end) so it has anotation that appear in view
namespace CompanyMVC.ViewModels
{
    public class EmployeeViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length 50 Characters")]
        [MinLength(5, ErrorMessage = "Minmum Length 5 Characters")]
        public string Name { get; set; }
        [Range(22, 50, ErrorMessage = "Age Must be in Range 22 To 50")]
        public int? Age { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public IFormFile Image { get; set; }//Carry Image To Image Folder(not in data base)
        public string ImageName { get; set; }//carry image name to database
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }//by default Not allow null value
                                              //if FK Optional => OnDelete Will be Restrict :if user want to delete any dept that have employees it willn't allow
                                              //if FK Required => OnDelete will be (Cascade) :if user remove any dept it will remove all employee in that dept
        public Department Departments { get; set; }//navigational of one
    }
}
