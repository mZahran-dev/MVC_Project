using Microsoft.AspNetCore.Mvc;
using MVC_Project_BLL.Interfaces;

namespace MVC_Project_PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository repository) // CLR will Create object
        {
            departmentRepository = repository;
        }
        public IActionResult Index()
        {
            // GetAll calling
            departmentRepository.GetAll();
            return View();
        }
    }
}
