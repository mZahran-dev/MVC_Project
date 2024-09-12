using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL.Models
{
    public enum Gender
    {
        [EnumMember(Value ="Male")]
        Male = 1 ,
        [EnumMember(Value = "Female")]
        Female =2
    }
    public enum EmployeeType
    {
        FullTime =1 ,
        PartTime =2
    }
    public class Employee : ModelClass
    {
        //public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        [MaxLength(50, ErrorMessage = "Max Length For Name is 50")]
        [MinLength(3, ErrorMessage ="Minimum Length For Name is 3 Characters")]
        public string Name { get; set; }
        [Range(21,60)]
        public int? Age { get; set; }
        [RegularExpression(@"^\d{1,5}-[A-Za-z\s]+-[A-Za-z\s]+-[A-Za-z\s]+$",ErrorMessage ="Address Must be like 123-street-city-country")]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary  { get; set; }
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name ="Hire Date")]
        public DateTime HireDate { get; set; }
        public bool isActive { get; set; }
        public bool IsDeleted { get; set; } //soft Delete 
        public Gender gender { get; set; }
    }
}
