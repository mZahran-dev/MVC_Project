using Microsoft.EntityFrameworkCore;
using MVC_Project_BLL.Interfaces;
using MVC_Project_DAL.Data;
using MVC_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext _dbContext) : base(_dbContext)
        {

        }
        public IQueryable<Employee> GetEmployeeByAddress(string address)
        {
             return _dbContext.Employees.Where(E =>  E.Address.ToLower().Contains(address.ToLower()));
        }
    }
}
