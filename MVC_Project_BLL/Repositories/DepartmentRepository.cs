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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _dbContext; //NULL
        public DepartmentRepository(AppDbContext dbContext) // ASK CLR For Creating object
        {
            _dbContext = dbContext;
        }
        public int delete(Department department)
        {
            _dbContext.Departments.Remove(department);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        {
            return _dbContext.Departments.AsNoTracking().ToList();  
        }

        public Department GetById(int id)
        {
            //var department = _dbContext.Departments.Where(D=>D.Id == id).FirstOrDefault();

            // another way but better performance
            return _dbContext.Departments.Find(id);
        }

        public int Add(Department department)
        {
            _dbContext.Departments.Add(department);
            return _dbContext.SaveChanges();
        }

        public int update(Department department)
        {
            _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}
