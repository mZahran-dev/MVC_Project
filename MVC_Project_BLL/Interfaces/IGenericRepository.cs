using MVC_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL.Interfaces
{
    public interface IGenericRepository<T> where T: ModelClass
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        int Add(T item);
        int update(T item);
        int delete(T item);
    }
}
