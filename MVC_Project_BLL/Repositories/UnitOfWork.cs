using MVC_Project_BLL.Interfaces;
using MVC_Project_DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext) // Level of injection
        {
            _dbContext = dbContext;
        }
        public IEmployeeRepository EmployeeRepository { get; set; } //Null
        public IDepartmentRepository DepartmentRepository { get; set; }

        public int Save()
        {
           return _dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
