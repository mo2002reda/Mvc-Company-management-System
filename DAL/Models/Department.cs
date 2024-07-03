using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DAL.Models
{
    public class Department
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "Department Name Is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Department Code Is Required")]
        [MaxLength(50)]
        public string Code { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        [InverseProperty("Departments")]//detect the navigational of the relation as we have many relation with 2 table
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        //Navigational prop of Many
    }
}
