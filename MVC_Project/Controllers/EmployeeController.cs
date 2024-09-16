using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using Microsoft.Extensions.Hosting;
using MVC_Project_BLL.Interfaces;
using MVC_Project_DAL.Models;
using System;

namespace MVC_Project_PL.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository repository,IWebHostEnvironment env)
        {
            employeeRepository = repository;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var emp = employeeRepository.GetAll();
            return View(emp);
        }

        #region Create Action
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"] = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {

                var count = employeeRepository.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(employee);
        }

        #endregion

        #region Details Action
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var emp = employeeRepository.GetById(id.Value);
            if (emp == null)
            {
                return NotFound();
            }
            return View(ViewName, emp);
        } 
        #endregion

        #region Edit Action
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View(employee);

            try
            {
                employeeRepository.update(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured during Update Employee");
                }
                return View(employee);
            }
        }

        #endregion

        #region Delete Action
        [HttpGet]

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee)
        {
            try
            {
                employeeRepository.delete(employee);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured during Delete Employee");
                }
                return View(employee);
            }
        }
        #endregion

    }
}
