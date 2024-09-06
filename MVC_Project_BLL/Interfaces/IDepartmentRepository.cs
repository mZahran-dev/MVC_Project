using MVC_Project_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        public IEnumerable<Department> GetAll();
        Department GetById(int id);
        int Add(Department department);
        int update(Department department);
        int delete(Department department);
    }
}
