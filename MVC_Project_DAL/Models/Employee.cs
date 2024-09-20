using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Name { get; set; }  
        public int? Age { get; set; }    
        public string Address { get; set; }
        public string Email { get; set; }
        public decimal Salary  { get; set; }  
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public bool isActive { get; set; }
        public bool IsDeleted { get; set; } //soft Delete 
        public Gender gender { get; set; }

        //navigation property
        //[InverseProperty(nameof(Models.Department.Employees))]
        public Department department { get; set; }
        public int? DepartmentId { get; set; } //FK Column
        public string ImageName { get; set; }
    }
}
