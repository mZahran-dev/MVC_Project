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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelClass
    {
        private protected readonly AppDbContext _dbContext; //NULL
        public GenericRepository(AppDbContext dbContext) // ASK CLR For Creating object
        {
            _dbContext = dbContext;
        }
        public void delete(T item)
        {
            _dbContext.Set<T>().Remove(item); //change state to Deleted
            //return _dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) _dbContext.Employees.Include(E => E.department).AsNoTracking().ToList();
            }
            else
            {
                return _dbContext.Set<T>().AsNoTracking().ToList();

            }
        }

        public T GetById(int id)
        {
            //var T = _dbContext.Ts.Where(D=>D.Id == id).FirstOrDefault();

            // another way but better performance
            return _dbContext.Set<T>().Find(id);
        }

        public void Add(T item)
        {
            _dbContext.Add(item);  //change state to Added
            //return _dbContext.SaveChanges();
        }

        public void update(T item)
        {
            _dbContext.Set<T>().Update(item);  //change state to Modified
            //return _dbContext.SaveChanges();
        }
    }
}
