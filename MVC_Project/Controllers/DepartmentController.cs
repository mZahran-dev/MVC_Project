using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MVC_Project_BLL.Interfaces;
using MVC_Project_DAL.Models;

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
            var departmentIndex =  departmentRepository.GetAll();
            return View(departmentIndex);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department) 
        {
            if (ModelState.IsValid)
            {
                var count = departmentRepository.Add(department);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var department = departmentRepository.GetById(id.Value);
            if (department == null)
            {
                return NotFound();
            } 
            return View(department);
        }
    }
}
