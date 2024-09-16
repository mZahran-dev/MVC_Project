using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_DAL.Models
{
    public class Department : ModelClass
    {
        //public int Id { get; set; }
        [Required(ErrorMessage = "Code is required" )]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Display(Name ="Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        //Navigation property of Many Employees
        //[InverseProperty(nameof(Models.Employee.department))]
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    
    
    }
}
