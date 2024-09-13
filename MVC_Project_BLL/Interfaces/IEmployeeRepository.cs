using MVC_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
       IQueryable<Employee> GetEmployeeByAddress(string address);
    }
}
