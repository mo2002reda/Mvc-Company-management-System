using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Name { get; set; }

        public int? Age { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        public string Email { get; set; }

        public string ImageName { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [ForeignKey("Departments")]
        public int? DepartmentId { get; set; }//by default Not allow null value
                                              //if FK Optional => OnDelete Will be Restrict :if user want to delete any dept that have employees it willn't allow
                                              //if FK Required => OnDelete will be (Cascade) :if user remove any dept it will remove all employee in that dept
        public Department Departments { get; set; }//navigational of one

    }
}
